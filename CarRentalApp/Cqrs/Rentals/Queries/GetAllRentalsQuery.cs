using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models;
using MediatR;

namespace CarRentalApp.Cqrs.Rentals.Queries
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
            private readonly IDataContext _dataContext;
            public Handler(IDataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<CQRSQueryResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var rentals = _dataContext.GetAllRentals();

                return rentals.Any() ?
                    new CQRSQueryResponse<Response>
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        QueryResult = new Response(rentals.ToList())
                    } :
                    new CQRSQueryResponse<Response>
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        ErrorMessage = "No items found!"
                    };
            }
        }
        public class Response
        {
            public IEnumerable<Rental> RentalsList { get; } = new List<Rental>();
            public Response(IEnumerable<Rental> rentalsList)
            {
                RentalsList = rentalsList;
            }
        }
    }
}
