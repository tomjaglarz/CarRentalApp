using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models;
using MediatR;

namespace CarRentalApp.Cqrs.Customers.Queries
{
    public class GetAllCustomersQuery
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
                var customers = _dataContext.Customers;

                return customers.Any() ?
                    new CQRSQueryResponse<Response>
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        QueryResult = new Response(customers.ToList())
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
            public List<Customer> CustomersList { get; } = new List<Customer>();
            public Response(List<Customer> rentalsList)
            {
                CustomersList = rentalsList;
            }
        }
    }
}
