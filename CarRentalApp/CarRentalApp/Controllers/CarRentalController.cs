using CarRentalApp.Commands;
using CarRentalApp.Data;
using CarRentalApp.Models;
using CarRentalApp.Queries;
using CarRentalApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarRentalController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
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

        


        //private IEnumerable<Rental> CarRentals()
        //{
        //    return new List<Rental>()
        //    {
        //        new Rental()
        //        {
        //            Id = 1,
        //            RentalNumber = 1,
        //            Customer = null,
        //            Car = null,
        //            DateFrom = DateTime.Now.Date,
        //            DateTo= DateTime.Now.AddDays(7).Date,
        //        },
        //        new Rental()
        //        {
        //            Id = 2,
        //            RentalNumber = 2,
        //            Customer = null,
        //            Car = null,
        //            DateFrom = DateTime.Now.Date,
        //            DateTo= DateTime.Now.AddDays(14).Date,
        //        }
        //    };
        //}
    }
}


//public int Id { get; set; }
//public int RentalNumber { get; set; }
//public int CustomerId { get; set; }
//public Car Car { get; set; }
//public DateTime DateFrom { get; set; }
//public DateTime DateTo { get; set; }