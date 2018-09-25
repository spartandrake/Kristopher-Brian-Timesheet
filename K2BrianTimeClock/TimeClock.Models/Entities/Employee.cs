using System;
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
    }
}
