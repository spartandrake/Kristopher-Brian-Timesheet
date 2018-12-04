using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet2.EntityFramework
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool ExemptFromOvertime { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double HourlyWage { get; set; }

        public string SupervisorId { get; set; }

        [ForeignKey(nameof(SupervisorId))]
        public ApplicationUser Supervisor { get; set; }

        [InverseProperty(nameof(ApplicationUser.Supervisor))]
        public List<ApplicationUser> Employees { get; set; }

        [InverseProperty(nameof(TimeSheet.User))]
        public List<TimeSheet> TimeSheets { get; set; }
    }
}

