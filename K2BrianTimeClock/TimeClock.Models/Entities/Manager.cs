using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeClock.Models.Entities
{
    [Table("Managers", Schema = "TimeClock")]
    public class Manager : Employee
    {
        [InverseProperty(nameof(Employee.Supervisor))]
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
