using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheet2.EntityFramework.Base;

namespace TimeSheet2.EntityFramework
{
    public class TimeSheet : EntityBase
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DefaultValue(ApprovalType.Pending)]
        public ApprovalType Approved { get; set; }

        public string ReasonDenied { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public bool ExemptFromOvertime { get; set; }

        //Start Navigation and keys

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [InverseProperty(nameof(ClockIn.TimeSheet))]
        public virtual List<ClockIn> ClockIns { get; set; }
    }
}

