namespace CarRentalApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string ModelName { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionTypeId { get; set; }
        public int SeatsCount { get; set; }
        public int CategoryId { get; set; }
        public int SpecsId { get; set; }

    }
}
