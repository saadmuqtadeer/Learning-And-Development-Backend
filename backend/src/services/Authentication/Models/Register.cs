using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models
{
    public class Register
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? Role { get; set; }
    }
}
