using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models;
using MediatR;

namespace CarRentalApp.Cqrs.Cars.Queries
{
    public class GetAllCarsQuery
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
                var cars = _dataContext.Cars;

                return cars.Any() ?
                    new CQRSQueryResponse<Response>
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        QueryResult = new Response(cars.ToList())
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
            public List<Car> CarsList { get; } = new List<Car>();
            public Response(List<Car> carsList)
            {
                CarsList = carsList;
            }
        }
    }
}
