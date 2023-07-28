using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models;
using MediatR;

namespace CarRentalApp.Queries
{
    public class GetAllRentalsQuery
    {
        public class Query : IRequest<CQRSQueryResponse<Response>>
        {
            public Query()
            {
            }
        }

        public class Handler : IRequestHandler<Query, CQRSQueryResponse<Response>>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<CQRSQueryResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var rentals = _dataContext.Rentals;

                return rentals.Any() ?
                    new CQRSQueryResponse<Response> { StatusCode = System.Net.HttpStatusCode.OK, QueryResult = new Response(rentals.ToList()) } :
                    new CQRSQueryResponse<Response> { StatusCode = System.Net.HttpStatusCode.NotFound, ErrorMessage = "No items found!" };
            }
        }
        public class Response
        {
            public List<Rental> RentalsList { get; } = new List<Rental>();
            public Response(List<Rental> rentalsList)
            {
                RentalsList = rentalsList;
            }
        }
    }
}
