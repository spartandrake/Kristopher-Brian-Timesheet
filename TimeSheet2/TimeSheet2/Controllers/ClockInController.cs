
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace TimeSheet2.Controllers
{
    
    [Route("[controller]/[action]")]
    public class ClockInController : Controller
    {
        //[Authorize(Roles = "Employee, Manager, HRManager, Administator")]
        public IActionResult ClockIn()
        {
            
            return View();
        }
    }
}
