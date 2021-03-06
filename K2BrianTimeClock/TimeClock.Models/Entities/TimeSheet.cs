﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TimeClock.Models.Entities.Base;

namespace TimeClock.Models.Entities
{
    [Table("TimeSheets", Schema = "TimeClock")]
    public class TimeSheet : EntityBase
    {
        public ClockIn[] ClockIns { get; set; }

        public int HoursWorked { get; set; }

        public int TotalPay { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public decimal CurrentWage { get; set; }
    }
}
