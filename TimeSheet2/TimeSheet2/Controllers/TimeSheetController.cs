using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet2.EF;
using TimeSheet2.EntityFramework;
using TimeSheet2.ViewModels.TimeSheetViewModels;

namespace TimeSheet2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class TimeSheetController : Controller
    {
        private double OverTimePay;
        private double OverTimeLimit;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TimeSheetController(UserManager<ApplicationUser> userManager)
        {
            _context = new ApplicationDbContext();
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListAllTimeSheets()
        {
            var timesheets = new List<TimeSheetViewModel>();
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var retrieved = _context.TimeSheets.Where(x => x.UserId == user.Id).OrderBy(x => x.StartDate);

            foreach (var timesheet in retrieved)
            {
                var timeWorked = TimeSheetTimeSpent(timesheet);
                var amountMade = TimeSheetAmountMade(timesheet, timeWorked);

                timesheets.Add(new TimeSheetViewModel
                {
                    StartDate = timesheet.StartDate,
                    EndDate = timesheet.EndDate,
                    Exempt = timesheet.ExemptFromOvertime,
                    Approved = timesheet.Approved,
                    TotalEarning = amountMade,
                    TotalHours = timeWorked,
                    DenialReason = timesheet.ReasonDenied ?? "Not Denied Yet",
                    TimesheetId = timesheet.Id
                });
            }
        }

        [HttpGet]
        public IActionResult ListPendingTimeSheets()
        {

        }

        [HttpGet]
        public IActionResult ListApprovedTimeSheets()
        {

        }

        [HttpGet]
        public IActionResult ListDeniedTimeSheets()
        {

        }

        [HttpGet]
        public IActionResult DetailTimeSheet(int id)
        {

        }

        [HttpGet]
        public IActionResult CurrentTimeSheet()
        {

        }

        private double TimeSheetTimeSpent(TimeSheet timesheet)
        {

        }

        private double TimeSheetAmountMade(TimeSheet timesheet, double timespent)
        {

        }

        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorsPendingTimeSheets()
        {

        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorsAllTimeSheets()
        {

        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorReviewTimesheet(int id)
        {

        }

        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorReviewTimesheet(SupervisorDetailTimeSheetViewModel model)
        {

        }

        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult AllWeeklyReport()
        {
            
        }
        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult GroupsWeekReport(DateTime startDate, DateTime endDate)
        {

        }

        [HttpGet]
        public IActionResult GroupsTimeCards(DateTime startDate, DateTime endDate, string supervisorId)
        {

        }

        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult CheckForTimesheet(int id)
        {


        }

        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult AllPendingTimesheets()
        {

        }

        private double GetWeeklyEarnings(DateTime startDate, DateTime endDate)
        {

        }

        private double GetWeeklyGroupEarnings(DateTime startDate, DateTime endDate, string supervisorId)
        {

        }

        //[Authorize(Roles = "Employee, Manager, HRManager, Administator")]
        public IActionResult TimeSheet()
        {
            
            
            return View();
        }
        
    }
}
