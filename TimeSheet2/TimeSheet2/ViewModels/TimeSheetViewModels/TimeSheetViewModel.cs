﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheet2.ViewModels.TimeSheetViewModels
{
    public class TimeSheetViewModel
    {
        public class WeeklyReportViewModel
        {
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
            public DateTime TotalHours { get; set; }

            [Display(Name = "Company Wages Paid")]
            public double TotalEarning { get; set; }
        }
    }
}