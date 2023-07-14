using CarRentalApp.Data;
using CarRentalApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CarRentalController(DataContext dataContext)
        {
            _dataContext= dataContext;
        }
        
        [HttpGet]
        public IEnumerable<Rental> Get()
        {
            return CarRentals();
            //return _dataContext.Rentals;
        }

        [HttpGet("{id}", Name = "Get")]
        public Rental GetCarRental(int id)
        {
            return CarRentals().FirstOrDefault(rental => rental.Id == id);
        }

        private IEnumerable<Rental> CarRentals()
        {
            return new List<Rental>()
            {
                new Rental()
                {
                    Id = 1,
                    RentalNumber = 1,
                    CustomerId = 1,
                    Car = null,
                    DateFrom = DateTime.Now.Date,
                    DateTo= DateTime.Now.AddDays(7).Date,
                },
                new Rental()
                {
                    Id = 2,
                    RentalNumber = 2,
                    CustomerId = 2,
                    Car = null,
                    DateFrom = DateTime.Now.Date,
                    DateTo= DateTime.Now.AddDays(14).Date,
                }
            };
        }
    }
}


//public int Id { get; set; }
//public int RentalNumber { get; set; }
//public int CustomerId { get; set; }
//public Car Car { get; set; }
//public DateTime DateFrom { get; set; }
//public DateTime DateTo { get; set; }