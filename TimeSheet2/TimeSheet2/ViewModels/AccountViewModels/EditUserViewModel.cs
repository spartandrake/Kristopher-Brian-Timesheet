using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TimeSheet2.ViewModels.AccountViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string UserId { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Set a New First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Set a New Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Set a New Email")]
        public string Email { get; set; }

        public string Role { get; set; }

        [Required]
        [Display(Name="Hourly Wage")]
        public double HourlyWage { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Set a New Password")]
        public string Password { get; set; }
        
        [Display(Name = "Supervisor")]
        public string SupervisorId { get; set; }
    }
}
