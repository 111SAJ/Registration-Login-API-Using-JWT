using System.ComponentModel.DataAnnotations;

namespace RegLog.Model
{
    public class Login
    {
        [Required (ErrorMessage = "Email is required")]
        [EmailAddress (ErrorMessage = "Invalid email format")]
        public string? EmployeeEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public bool isLoggedIn { get; set; } = false;
    }
}
