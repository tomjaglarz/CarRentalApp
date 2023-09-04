using CarRentalApp.Configurations;
using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Logic.Login;
using CarRentalApp.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalApp.Cqrs.Identity.Queries
{
    public class LogInQuery
    {
        public class Query : IRequest<CQRSQueryResponse<LoginResponse>>
        {
            public UserLoginRequest UserLoginRequest { get; }
            public Query(UserLoginRequest userLoginRequest)
            {
                UserLoginRequest = userLoginRequest;
            }
        }

        public class Handler : IRequestHandler<Query, CQRSQueryResponse<LoginResponse>>
        {

            private readonly IDataContext _dataContext;
            private readonly UserManager<IdentityUser> _userManager;
            private readonly JwtConfig _jwtConfig;
            public Handler(IDataContext dataContext, UserManager<IdentityUser> userManager, IOptions<JwtConfig> jwtConfig)
            {
                _dataContext = dataContext;
                _userManager = userManager;
                _jwtConfig = jwtConfig.Value;
            }
            public async Task<CQRSQueryResponse<LoginResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.UserLoginRequest.Email);

                if (user == null)
                {
                    return new CQRSQueryResponse<LoginResponse>
                    {
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        QueryResult = new LoginResponse(null, false, "User not found"),
                        ErrorMessage = "Login failed"
                    };
                }

                var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.UserLoginRequest.Password); 

                if (!isPasswordCorrect)
                {
                    return new CQRSQueryResponse<LoginResponse>
                    {
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        QueryResult = new LoginResponse(null, false, "Invalid credentials"),
                        ErrorMessage = "Login failed"
                    };
                }

                var jwtToken = GenerateJwtToken(user);
                return new CQRSQueryResponse<LoginResponse>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    QueryResult = new LoginResponse(jwtToken, true, "User logged in"),
                };
            }

            private string GenerateJwtToken(IdentityUser user)
            {
                var tokenHanlder = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub, user.Email),
                    new(JwtRegisteredClaimNames.Email, user.Email)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _jwtConfig.Issuer,
                    Audience = _jwtConfig.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHanlder.CreateToken(tokenDescriptor);
                var jwtToken = tokenHanlder.WriteToken(token);
                return jwtToken;
            }
        }
    }
    public class LoginResponse
    {
        public string? Token { get; }
        public bool IsAuthorized { get; }
        public string Message { get; }

        public LoginResponse(string token, bool isAuthorized, string message)
        {
            Token = token;
            IsAuthorized = isAuthorized;
            Message = message;
        }
    }

}
