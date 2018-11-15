using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeClock.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeClock.MVC.Models
{
    public class Employee : ApplicationUser
    {

        public string EmployeeFirstName { get; set; }

   
        public string EmployeeLastName { get; set; }

        public decimal CurrentWage { get; set; }


        public string EmailAddress { get; set; }


        public string Password { get; set; }

    }
}
