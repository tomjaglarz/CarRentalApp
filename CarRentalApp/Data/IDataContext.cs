using CarRentalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Data
{
    public interface IDataContext
    {
        DbSet<Car> Cars { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Rental> Rentals { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IList<Rental> GetAllRentals();
    }
}