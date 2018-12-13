using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet2.EntityFramework;

namespace TimeSheet2.ViewModels.TimeSheetViewModels
{
    public class TimeSheetViewModel
    {
        [Required]
        public int TimeSheetId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Total Hours Worked")]
        public double TotalHours { get; set; }

        [Display(Name = "Total Earnings")]
        public double TotalEarning { get; set; }

        [Display(Name = "Exempt from Overtime")]
        public bool Exempt { get; set; }
        
        public ApprovalType Approved { get; set; }

        public string DenialReason { get; set; }
    }
}
