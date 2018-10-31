using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TimeClock.Models.Entities.Base;

namespace TimeClock.Models.Entities
{
    [Table("ClockIns", Schema = "TimeClock")]
    public class ClockIn : EntityBase
    {
        [Display(Name = "Time Clocked In")]
        public DateAndTime TimeIn { get; set; }

        [Display(Name = "Time Clocked Out")]
        public DateAndTime TimeOut { get; set; }

        [Display(Name = "Total Hours")]
        public decimal HoursWorked { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("TimeSheetId")]
        public TimeSheet TimeSheet { get; set; }
    }
}
