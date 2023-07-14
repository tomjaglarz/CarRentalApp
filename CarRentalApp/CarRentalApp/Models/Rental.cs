namespace CarRentalApp.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int RentalNumber { get; set; }
        public int CustomerId { get; set; }
        public Car Car { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
