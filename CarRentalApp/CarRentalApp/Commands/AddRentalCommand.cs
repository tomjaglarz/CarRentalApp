using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models;
using CarRentalApp.ViewModels;
using MediatR;
using System.Net;
using System.Windows.Input;

namespace CarRentalApp.Commands
{
    public static class AddRentalCommand
    {
        public record Command(RentalViewModel RentalViewModel) : IRequest<CQRSCommandResponse>;

        public class Handler : IRequestHandler<Command, CQRSCommandResponse>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<CQRSCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var rental = new Rental()
                {
                    DateFrom = request.RentalViewModel.DateFrom,
                    DateTo = request.RentalViewModel.DateTo,
                    CarId = request.RentalViewModel.CarId,
                    CustomerId = request.RentalViewModel.CustomerId,
                };

                _dataContext.Rentals.Add(rental);
                _dataContext.SaveChanges();

                if (rental.Id > 0)
                    return new CQRSCommandResponse { StatusCode = HttpStatusCode.OK, ReturnedId = rental.Id };

                return new CQRSCommandResponse { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "Could not save new rental" };
            }
        }
    }
}
