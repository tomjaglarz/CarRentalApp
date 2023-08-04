using CarRentalApp.Cqrs.Rentals.Queries;
using CarRentalApp.Data;
using CarRentalApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CarRentalApp.Cqrs.Rentals.Queries.GetAllRentalsQuery;

namespace CarRentalApp.Tests
{
    public class GetAllRentalsQueryTests
    {
        private readonly Mock<IDataContext> _dataContextMoq;
        private readonly Query _query;
        private readonly Handler _handler;
        public GetAllRentalsQueryTests()
        {
            _dataContextMoq = new Mock<IDataContext>();
            _query = new Query();
            _handler = new Handler(_dataContextMoq.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_ListOfRentals()
        {
            _dataContextMoq.Setup(x => x.GetAllRentals()).Returns(new List<Rental>()
            {
                new Rental{
                    Id = 1,
                    CustomerId = 1,
                    CarId = 1,
                    DateFrom = default,
                    DateTo = default
                }
            });

            var response = await _handler.Handle(_query, default);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<List<Rental>>(response.QueryResult.RentalsList);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_No_Data()
        {
            _dataContextMoq.Setup(x => x.GetAllRentals()).Returns(Enumerable.Empty<Rental>().ToList());

            var response = await _handler.Handle(_query, default);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
