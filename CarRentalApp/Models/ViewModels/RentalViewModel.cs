namespace CarRentalApp.Models.ViewModels
{
    public class RentalViewModel
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
}
