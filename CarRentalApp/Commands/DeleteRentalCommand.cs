using CarRentalApp.Data;
using CarRentalApp.Logic;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalApp.Commands
{
    public class DeleteRentalCommand
    {
        public record Command(int Id) : IRequest<CQRSCommandResponse>;

        public class Handler : IRequestHandler<Command, CQRSCommandResponse>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext dataContext)
            {
                _dataContext= dataContext;
            }

            public async Task<CQRSCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var rentalToDelete = _dataContext.Rentals.FirstOrDefault(r => r.Id == request.Id);
                if (rentalToDelete == null)
                {
                    return new CQRSCommandResponse { StatusCode = System.Net.HttpStatusCode.NotFound, ErrorMessage = "Not found" };
                }

                _dataContext.Rentals.Remove(rentalToDelete);
                var saveResult = await _dataContext.SaveChangesAsync();

                return saveResult > 0
                    ? new CQRSCommandResponse { StatusCode = System.Net.HttpStatusCode.OK, ReturnedId = request.Id }
                    : new CQRSCommandResponse { StatusCode = System.Net.HttpStatusCode.NotFound, ErrorMessage = "Could not delete" };
            }
        }
    }
}
