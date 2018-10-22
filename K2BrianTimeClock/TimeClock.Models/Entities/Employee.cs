using System;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeClock.Models.Entities
{
    [Table("Employees", Schema = "TimeClock")]
    public class Employee : EntityBase
    {
        [DataType(DataType.Text), MaxLength(50), Display(Name = "First Name")]
        public string EmployeeFirstName { get; set; }

        [DataType(DataType.Text), MaxLength(50), Display(Name = "Last Name")]
        public string EmployeeLastName { get; set; }

        [DataType(DataType.Currency), MaxLength(4)]
        public decimal Wage { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress), MaxLength(50), Display(Name = "Email Address")]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsExempt { get; set; }

        [ForeignKey("ManagerId")]
        public Manager Supervisor { get; set; }

        [InverseProperty(nameof(TimeSheet.Employee))]
        public TimeSheet[] PreviousTimeSheets { get; set; }

        [ForeignKey("TimeSheetId")]
        public TimeSheet CurrentTimeSheet { get; set; }
    }
}
