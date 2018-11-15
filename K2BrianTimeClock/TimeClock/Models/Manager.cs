using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeClock.MVC.Models
{
    public class Manager : Employee
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
