using CarRentalApp.Cqrs.Rentals.Queries;
using CarRentalApp.Data;
using CarRentalApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Tests
{
    public class GetAllRentalsQueryTests
    {
        //private readonly Mock<DataContext> _dataContextMoq;
        //public GetAllRentalsQueryTests()
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //    .UseInMemoryDatabase(databaseName: "RentalAppDb")
        //    .Options;

        //    _dataContextMoq = new Mock<DataContext>(options);
        //}
        //[Fact]
        //public async Task Handle_Should_Return_ListOfRentals()
        //{
        //    _dataContextMoq.Setup(x => x.GetAllRentals()).Returns(new List<Rental>()
        //    {
        //        new Rental{ 
        //            Id = 1,
        //            CustomerId = 1,
        //            CarId = 1,
        //            DateFrom = default,
        //            DateTo = default
        //        }
        //    });
        //    var command = new GetAllRentalsQuery.Query();
        //    var handler = new GetAllRentalsQuery.Handler(_dataContextMoq.Object);

        //    var response = await handler.Handle(command, default);

        //    Assert.IsType<List<Rental>>(response.QueryResult);
        //}
    }
}
