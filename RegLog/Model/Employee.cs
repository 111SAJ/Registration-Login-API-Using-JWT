using System.ComponentModel.DataAnnotations;

namespace RegLog.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? EmployeeName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? EmployeeEmail { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 characters")]
        public string? Password { get; set; }
    }
}
