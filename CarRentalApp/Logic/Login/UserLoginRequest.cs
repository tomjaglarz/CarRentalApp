using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Logic.Login
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
