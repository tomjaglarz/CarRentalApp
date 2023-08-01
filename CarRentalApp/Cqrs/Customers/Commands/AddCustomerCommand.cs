using CarRentalApp.Data;
using CarRentalApp.Logic;
using CarRentalApp.Models;
using CarRentalApp.Models.ViewModels;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace CarRentalApp.Cqrs.Customers.Commands
{
    public class AddCustomerCommand
    {
        public record Command(CustomerViewModel CustomerViewModel) : IRequest<CQRSCommandResponse>;

        public class Handler : IRequestHandler<Command, CQRSCommandResponse>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext dataContext) 
            {
                _dataContext = dataContext;
            }
            public async Task<CQRSCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                Customer customer = new Customer()
                {
                    FirstName = request.CustomerViewModel.FirstName,
                    LastName = request.CustomerViewModel.LastName,
                    Adress = request.CustomerViewModel.Adress,
                    Email = request.CustomerViewModel.Email,
                };

                _dataContext.Customers.Add(customer);
                await _dataContext.SaveChangesAsync(cancellationToken);

                if (customer.Id > 0)
                    return new CQRSCommandResponse { StatusCode = HttpStatusCode.OK, ReturnedId = customer.Id };

                return new CQRSCommandResponse { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "Could not save new customer" };

                throw new NotImplementedException();
            }
        }
    }
}
