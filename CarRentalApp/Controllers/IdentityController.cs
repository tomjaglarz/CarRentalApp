using CarRentalApp.Configurations;
using CarRentalApp.Cqrs.Identity.Commands;
using CarRentalApp.Cqrs.Identity.Queries;
using CarRentalApp.Cqrs.Rentals.Commands;
using CarRentalApp.Logic.Login;
using CarRentalApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalApp.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IdentityController(IMediator mediator )
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserLoginRequest userLoginRequest)
        {
            var response = await _mediator.Send(new RegisterUserCommand.Command(userLoginRequest));
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequest userLoginRequest)
        {
            var response = await _mediator.Send(new LogInQuery.Query(userLoginRequest));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response);

            return Unauthorized(response);
        }

    }
}
