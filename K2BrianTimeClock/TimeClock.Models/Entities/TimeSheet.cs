using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TimeClock.Models.Entities.Base;

namespace TimeClock.Models.Entities
{
    [Table("TimeSheet", Schema = "TimeClock")]
    class TimeSheet : EntityBase
    {
        public ClockTime[] ClockIns { get; set; }
        public int HoursWorked { get; set; }
        public int TotalPay { get; set; }

    }
}
