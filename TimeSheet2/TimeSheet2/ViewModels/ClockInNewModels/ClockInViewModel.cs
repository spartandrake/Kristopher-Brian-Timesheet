using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TimeSheet2.ViewModels.ClockInViewModels
{
    public class ClockInViewModel
    {
        [Display(Name = "Welcome,")]
        public string UserName { get; set; }


        [Display(Name = "On Clock")]
        [DefaultValue(false)]
        public bool OnClock { get; set; }

       
        public string EmployeeId { get; set; }

        

        




    }
}