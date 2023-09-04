using CarRentalApp.Cqrs.Cars.Queries;
using CarRentalApp.Cqrs.Cars.Commands;
using CarRentalApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllCars")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await _mediator.Send(new GetAllCarsQuery.Query());

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarViewModel carViewModel)
        {
            return Ok(await _mediator.Send(new AddCarCommand.Command(carViewModel)));
        }
    }
}
