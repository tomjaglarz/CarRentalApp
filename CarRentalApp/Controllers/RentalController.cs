using CarRentalApp.Cqrs.Rentals.Commands;
using CarRentalApp.Cqrs.Rentals.Queries;
using CarRentalApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers
{

    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet]
        [Route("GetAllRentals")]
        public async Task<IActionResult> GetAllRentals()
        {
            var response = await _mediator.Send(new GetAllRentalsQuery.Query());

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [HttpGet]
        [Route("GetRentalById")]
        public async Task<IActionResult> GetRetnalById(int id)
        {
            var response = await _mediator.Send(new GetRentalByIdQuery.Query(id));

            if (response != null)
                return Ok(response);

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddRental([FromBody] RentalViewModel rentalViewModel)
        {
            return Ok(await _mediator.Send(new AddRentalCommand.Command(rentalViewModel)));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateRental(int id, [FromBody] RentalViewModel rentalViewModel)
        {
            var response = await _mediator.Send(new UpdateRentalCommand.Command(id, rentalViewModel));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response);
            
            return NotFound(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var response = await _mediator.Send(new DeleteRentalCommand.Command(id));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response);
            
            return NotFound(response);
        }
    }
}