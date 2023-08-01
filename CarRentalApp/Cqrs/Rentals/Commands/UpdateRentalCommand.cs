using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models.ViewModels;
using MediatR;

namespace CarRentalApp.Cqrs.Rentals.Commands
{
    public class UpdateRentalCommand
    {
        public record Command(int Id, RentalViewModel RentalViewModel) : IRequest<CQRSCommandResponse>;

        public class Handler : IRequestHandler<Command, CQRSCommandResponse>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<CQRSCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var rentalToUpdate = _dataContext.Rentals.FirstOrDefault(r => r.Id == request.Id);
                if (rentalToUpdate == null)
                {
                    return new CQRSCommandResponse { StatusCode = System.Net.HttpStatusCode.NotFound, ErrorMessage = $"Rental with id {request.Id} not found!" };
                }
                else
                {
                    rentalToUpdate.CarId = request.RentalViewModel.CarId;
                    rentalToUpdate.CustomerId = request.RentalViewModel.CustomerId;
                    rentalToUpdate.DateFrom = request.RentalViewModel.DateFrom;
                    rentalToUpdate.DateTo = request.RentalViewModel.DateTo;
                }
                var saveResult = await _dataContext.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? new CQRSCommandResponse { StatusCode = System.Net.HttpStatusCode.OK, ReturnedId = request.Id }
                    : new CQRSCommandResponse { StatusCode = System.Net.HttpStatusCode.NotFound, ErrorMessage = "Could not update" };
            }
        }

    }
}
