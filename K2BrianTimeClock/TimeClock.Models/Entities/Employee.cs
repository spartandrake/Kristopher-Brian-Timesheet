using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeClock.Models.Entities
{
    [Table("Employees", Schema = "TimeClock")]
    class Employee : EntityBase
    {
        [DataType(DataType.Text), MaxLength(50)]
        public string EmployeeFirstName { get; set; }

        [DataType(DataType.Text), MaxLength(50)]
        public string EmployeeLastName { get; set; }

        [DataType(DataType.Currency), MaxLength(4)]
        public string Wage { get; set; }

        [DataType(DataType.Text), MaxLength(24)]
        public string Username { get; set; }

        public Boolean IsActive { get; set; }
        public Boolean IsExempt { get; set; }
        public Manager Supervisor { get; set; }
        public TimeSheet[] PreviousTimeSheets { get; set; } 




    }
}
