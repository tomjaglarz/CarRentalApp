using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string ModelName { get; set; }
        public int SeatsCount { get; set; }

    }
}
