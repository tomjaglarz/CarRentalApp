using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models.ViewModels;
using CarRentalApp.Models;
using MediatR;
using System.Net;

namespace CarRentalApp.Cqrs.Cars.Commands
{
    public class AddCarCommand
    {
        public record Command(CarViewModel CarViewModel) : IRequest<CQRSCommandResponse>;

        public class Handler : IRequestHandler<Command, CQRSCommandResponse>
        {
            private readonly IDataContext _dataContext;
            public Handler(IDataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<CQRSCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                Car car = new Car()
                {
                    Brand = request.CarViewModel.Brand,
                    ModelName = request.CarViewModel.ModelName,
                    SeatsCount = request.CarViewModel.SeatsCount,
                };

                _dataContext.Cars.Add(car);
                await _dataContext.SaveChangesAsync(cancellationToken);

                if (car.Id > 0)
                    return new CQRSCommandResponse { StatusCode = HttpStatusCode.OK, ReturnedId = car.Id };

                return new CQRSCommandResponse { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "Could not save new car" };

                throw new NotImplementedException();
            }
        }
    }
}
