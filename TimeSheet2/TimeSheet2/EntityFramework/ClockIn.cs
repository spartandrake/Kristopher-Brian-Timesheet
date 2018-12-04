using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheet2.EntityFramework.Base;

namespace TimeSheet2.EntityFramework
{
    public class ClockIn : EntityBase
    {
        [Required]
        public DateTime ClockInTime { get; set; }

        public DateTime? ClockOutTime { get; set; }

        [Required]
        public int TimeSheetId { get; set; }

        [ForeignKey(nameof(TimeSheetId))]
        public TimeSheet TimeSheet { get; set; }

    }
}
