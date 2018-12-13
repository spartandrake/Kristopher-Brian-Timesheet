using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet2.EntityFramework;

namespace TimeSheet2.ViewModels
{
    public class PayrollTimeSheetViewModel
    {
        [Required]
        [HiddenInput]
        public int TimesheetId { get; set; }

        [Display(Name = "Owner")]
        public string User { get; set; }

        [Display(Name = "Supervisor")]
        public string Supervisor { get; set; }

        [Required]
        [Display(Name = "Approval")]
        public ApprovalType Approved { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Exempt From Overtime")]
        public bool ExemptFromOvertime { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Amount Made")]
        public double AmountMade { get; set; }

        [Display(Name = "Time Worked (Hourly)")]
        public double TimeWorked { get; set; }
    }
}
