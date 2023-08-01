using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models;
using MediatR;

namespace CarRentalApp.Cqrs.Rentals.Queries
{
    public class GetRentalByIdQuery
    {
        public class Query : IRequest<CQRSQueryResponse<Response>>
        {
            public int Id { get; }
            public Query(int id)
            {
                Id = id;
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
                var rental = _dataContext.Rentals.FirstOrDefault(x => x.Id == request.Id);

                return rental != null ?
                    new CQRSQueryResponse<Response>
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        QueryResult = new Response(rental)
                    } :
                    new CQRSQueryResponse<Response>
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        ErrorMessage = "No items found!"
                    };

            }
        }
    }

    public class Response
    {
        public Rental Rental { get; } = new Rental();
        public Response(Rental rental)
        {
            Rental = rental;
        }
    }
}
