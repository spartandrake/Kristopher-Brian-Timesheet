using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TimeClock.Models.Entities;
using TimeClock.Models.Entities.Base;

namespace TimeClock.Models.ViewModels
{
    class EmployeeWithCurrentTimeSheet : EntityBase
    {
        public ClockIn[] ClockIns { get; set; }

        public int HoursWorked { get; set; }

        [DataType(DataType.Currency), Display(Name = "Week's Pay")]
        public int TotalPay { get; set; }

        [DataType(DataType.Currency), Display(Name = "Employee Wage")]
        public decimal CurrentWage { get; set; }
    }
}
