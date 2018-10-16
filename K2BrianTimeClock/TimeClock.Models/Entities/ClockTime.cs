using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TimeClock.Models.Entities.Base;

namespace TimeClock.Models.Entities
{
    [Table("ClockTime", Schema = "TimeClock")]
    class ClockTime : EntityBase
    {
        public DateAndTime TimeIn { get; set; }
        public DateAndTime TimeOut { get; set; }
        public int EmployeeId { get; set; }
    }
}
