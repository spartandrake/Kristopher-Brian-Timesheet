using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TimeSheet2.EntityFramework;
using TimeSheet2.EF;
using TimeSheet2.ViewModels.ClockInViewModels;
using TimeSheet2.Util;

namespace TimeSheet2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ClockInController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClockInController(UserManager<ApplicationUser> userManager)
        {
            _context = new ApplicationDbContext();
            _userManager = userManager;
        }


        //[Authorize(Roles = "Employee, Manager, HRManager, Administator")]
        [HttpGet]
        public IActionResult ClockIn()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();
            var timeSheet = FindUserTimeSheet(user.Id);
            ClockIn clockIn = null;

            if(_context.ClockIns.Any(x => x.ClockOutTime == null && x.TimeSheetId == timeSheet.Id))
            {
                clockIn = _context.ClockIns.Last(x => x.ClockOutTime == null && x.TimeSheetId == timeSheet.Id);
            }

            bool clockedIn = false;
            if(clockIn != null)
            {
                clockedIn = true;
            }

            var viewModel = new ClockInViewModel {
                UserName = user.FirstName + " " + user.LastName,
                OnClock = clockedIn,
                EmployeeId = user.Id 
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ClockIn(bool In, ClockInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OnClock && In)
                {
                    ViewData["Info"] = "Currently Clocked In, clock out";
                    return View(model);
                }

                if (!model.OnClock && In)
                {
                    var clockIn = new ClockIn
                    {
                        ClockInTime = DateTime.Now,
                        TimeSheetId = model.TimeSheetId
                    };
                    _context.ClockIns.Add(clockIn);
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { In = true, Time = clockIn.ClockInTime });
                }
                if (model.OnClock && !In)
                {
                    var clockIn = _context.ClockIns.Last(x => x.TimeSheetId == model.TimeSheetId && x.ClockOutTime == null);
                    clockIn.ClockOutTime = DateTime.Now;
                    _context.ClockIns.Update(clockIn);
                    _context.SaveChanges();

                    return RedirectToAction("Index", new { In = false, Time = clockIn.ClockOutTime });
                }
            }
            return View(model);
        }

        private TimeSheet FindUserTimeSheet(string id)
        {
            var today = DateTime.Now;
            //DateTime.Today.StartOfWeek(StartOFTime);
            TimeSheet timeSheet;

            if (_context.TimeSheets.Any(x => x.UserId == id))
            {
                timeSheet = _context.TimeSheets
                .First(x => x.UserId == id && x.StartDate <= today && x.EndDate >= today);

            }
            else
            {
                timeSheet = CreateTimeSheet(id);
            }

            return timeSheet;
        }
        private TimeSheet CreateTimeSheet(string id)
        {
            var timesheet = new TimeSheet();
            var userRecord = _userManager.FindByIdAsync(id);

            timesheet.UserId = id;
            timesheet.StartDate = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
            timesheet.EndDate = timesheet.StartDate.AddDays(6);
            timesheet.ExemptFromOvertime = userRecord.Result.ExemptFromOvertime;

            //need to call save changes
            _context.TimeSheets.Add(timesheet);
            _context.SaveChanges();

            return timesheet;
        }
    }
}
