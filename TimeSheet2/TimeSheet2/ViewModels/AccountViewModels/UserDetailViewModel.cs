using System.ComponentModel.DataAnnotations;

namespace TimeSheet2.ViewModels.AccountViewModels
{
    public class UserDetailViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Hourly Wage")]
        public double HourlyWage { get; set; }

        public string Role { get; set; }
    }
}
