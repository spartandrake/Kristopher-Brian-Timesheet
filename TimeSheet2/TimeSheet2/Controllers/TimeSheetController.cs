﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheet2.Controllers
{
    [Route("[controller]/[action]")]
    public class TimeSheetController
    {    
        //[Authorize(Roles = "Employee, Manager, HRManager, Administator")]
        public IActionResult ViewTimeSheet()
        {

            return null;
        }
        
    }
}