﻿using CarRentalApp.Commands;
using CarRentalApp.Queries;
using CarRentalApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers
{
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


//public int Id { get; set; }
//public int RentalNumber { get; set; }
//public int CustomerId { get; set; }
//public Car Car { get; set; }
//public DateTime DateFrom { get; set; }
//public DateTime DateTo { get; set; }