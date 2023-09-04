using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models.ViewModels;
using CarRentalApp.Models;
using MediatR;
using System.Net;
using CarRentalApp.Logic.Login;
using Microsoft.AspNetCore.Identity;

namespace CarRentalApp.Cqrs.Identity.Commands
{
    public class RegisterUserCommand
    {
        public record Command(UserLoginRequest userLoginRequest) : IRequest<CQRSCommandResponse>;

        public class Handler : IRequestHandler<Command, CQRSCommandResponse>
        {
            private readonly IDataContext _dataContext;
            private readonly UserManager<IdentityUser> _userManager;
            public Handler(IDataContext dataContext, UserManager<IdentityUser> userManager)
            {
                _dataContext = dataContext;
                _userManager = userManager;
            }
            public async Task<CQRSCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var userExists = await _userManager.FindByEmailAsync(request.userLoginRequest.Email);
                if (userExists != null)
                {
                    return new CQRSCommandResponse { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = "User already exists" };
                }

                var newUser = new IdentityUser
                {
                    UserName = request.userLoginRequest.Email,
                    Email = request.userLoginRequest.Email,
                };

                var isCreated = await _userManager.CreateAsync(newUser, request.userLoginRequest.Password);
                if (isCreated.Succeeded) 
                {
                    return new CQRSCommandResponse { StatusCode = HttpStatusCode.Created, ErrorMessage = "User registered succesfully" };
                }

                return new CQRSCommandResponse { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = "Error" };
            }
        }
    }
}
