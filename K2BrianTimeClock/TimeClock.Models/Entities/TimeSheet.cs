using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TimeClock.Models.Entities.Base;

namespace TimeClock.Models.Entities
{
    [Table("TimeSheet", Schema = "TimeClock")]
    public class TimeSheet : EntityBase
    {
        public ClockTime[] ClockIns { get; set; }
        public int HoursWorked { get; set; }
        public int TotalPay { get; set; }
    }
}
