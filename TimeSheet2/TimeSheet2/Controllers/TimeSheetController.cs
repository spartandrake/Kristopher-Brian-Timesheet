using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet2.EF;
using TimeSheet2.EntityFramework;
using TimeSheet2.ViewModels;
using TimeSheet2.ViewModels.TimeSheetViewModels;

namespace TimeSheet2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class TimeSheetController : Controller
    {
        private readonly double OverTimePay = 1.5;
        private readonly double OverTimeLimit = 40; 

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

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
            return View("ListTimeSheet", timesheets);
        }

        [HttpGet]
        public IActionResult ListPendingTimeSheets()
        {
            var timesheets = new List<TimeSheetViewModel>();
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var retrieved = _context.TimeSheets.Where(x => x.UserId == user.Id && x.Approved == ApprovalType.Waiting).OrderBy(x => x.StartDate);

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


            return View("ListTimeSheet", timesheets);
        }

        [HttpGet]
        public IActionResult ListApprovedTimeSheets()
        {
            var timesheets = new List<TimeSheetViewModel>();
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var retrieved = _context.TimeSheets.Where(x => x.UserId == user.Id && x.Approved == ApprovalType.Approved).OrderBy(x => x.StartDate);

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


            return View("ListTimeSheet", timesheets);
        }

        [HttpGet]
        public IActionResult ListDeniedTimeSheets()
        {
            var timesheets = new List<TimeSheetViewModel>();
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var retrieved = _context.TimeSheets.Where(x => x.UserId == user.Id && x.Approved == ApprovalType.Denied).OrderBy(x => x.StartDate);

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


            return View("ListTimeSheet", timesheets);
        }

        [HttpGet]
        public IActionResult DetailTimeSheet(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var timesheet = _context.TimeSheets.Find(id);
            var timeWorked = TimeSheetTimeSpent(timesheet);
            var amountMade = TimeSheetAmountMade(timesheet, timeWorked);


            var model = new TimeSheetViewModel
            {
                StartDate = timesheet.StartDate,
                EndDate = timesheet.EndDate,
                Exempt = timesheet.ExemptFromOvertime,
                Approved = timesheet.Approved,
                TotalEarning = amountMade,
                TotalHours = timeWorked,
                DenialReason = timesheet.ReasonDenied ?? "Not Denied Yet"
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult CurrentTimeSheet()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var timesheet = _context.TimeSheets.Last(x => x.UserId == user.Id);
            var timeWorked = TimeSheetTimeSpent(timesheet);
            var amountMade = TimeSheetAmountMade(timesheet, timeWorked);


            var model = new TimeSheetViewModel
            {
                StartDate = timesheet.StartDate,
                EndDate = timesheet.EndDate,
                Exempt = timesheet.ExemptFromOvertime,
                Approved = timesheet.Approved,
                TotalEarning = amountMade,
                TotalHours = timeWorked,
                DenialReason = timesheet.ReasonDenied ?? "Not Denied Yet"
            };

            return View("DetailTimeSheet", model);
        }

        private double TimeSheetTimeSpent(TimeSheet timesheet)
        {
            if (timesheet == null) throw new ArgumentNullException(nameof(timesheet));

            var clockIns = _context.ClockIns.Where(x => x.TimeSheetId == timesheet.Id);

            TimeSpan totalTime = TimeSpan.Zero;

            foreach (var clockIn in clockIns)
            {
                var timespent = clockIn.ClockOutTime - clockIn.ClockInTime;
                totalTime = totalTime.Add(timespent.GetValueOrDefault());
            }

            return Math.Ceiling(totalTime.TotalMinutes / 60);
        }

        private double TimeSheetAmountMade(TimeSheet timesheet, double timespent)
        {
            var user = _userManager.FindByIdAsync(timesheet.UserId).Result;
            double amountMade = 0;

            if (timespent <= OverTimeLimit)
            {
                amountMade = timespent * user.HourlyWage;
            }
            else
            {
                var over = timespent - OverTimeLimit;
                amountMade = over * (user.HourlyWage * OverTimePay);
                amountMade += (timespent - over) * user.HourlyWage;
            }

            return amountMade;
        }

        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorsPendingTimeSheets()
        {
            var timesheets = new List<SupervisorTimeSheetViewModel>();
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var retrieved = _context.TimeSheets.Where(x => x.User.SupervisorId == user.Id
                                                           && x.Approved == ApprovalType.Waiting)
                .OrderBy(x => x.StartDate);

            foreach (var timesheet in retrieved)
            {
                var timeWorked = TimeSheetTimeSpent(timesheet);
                var amountMade = TimeSheetAmountMade(timesheet, timeWorked);
                var timesheetUser = _userManager.FindByIdAsync(timesheet.UserId).Result;

                timesheets.Add(new SupervisorTimeSheetViewModel
                {
                    StartDate = timesheet.StartDate,
                    EndDate = timesheet.EndDate,
                    Exempt = timesheet.ExemptFromOvertime,
                    Approved = timesheet.Approved,
                    TotalEarning = amountMade,
                    TotalHours = timeWorked,
                    DenialReason = timesheet.ReasonDenied ?? "Not Denied Yet",
                    TimesheetId = timesheet.Id,
                    User = timesheetUser.Email
                });
            }


            return View("UserManagerListTimeSheets", timesheets);
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorsAllTimeSheets()
        {
            var timesheets = new List<SupervisorTimeSheetViewModel>();
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var retrieved = _context.TimeSheets.Where(x => x.User.SupervisorId == user.Id)
                .OrderBy(x => x.StartDate);

            foreach (var timesheet in retrieved)
            {
                var timeWorked = TimeSheetTimeSpent(timesheet);
                var amountMade = TimeSheetAmountMade(timesheet, timeWorked);
                var timesheetUser = _userManager.FindByIdAsync(timesheet.UserId).Result;

                timesheets.Add(new SupervisorTimeSheetViewModel
                {
                    StartDate = timesheet.StartDate,
                    EndDate = timesheet.EndDate,
                    Exempt = timesheet.ExemptFromOvertime,
                    Approved = timesheet.Approved,
                    TotalEarning = amountMade,
                    TotalHours = timeWorked,
                    DenialReason = timesheet.ReasonDenied ?? "Not Denied Yet",
                    TimesheetId = timesheet.Id,
                    User = timesheetUser.Email
                });
            }


            return View(timesheets);
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorReviewTimesheet(int id)
        {
            var supervisor = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var timesheet = _context.TimeSheets.Find(id);
            var timesheetUser = _userManager.FindByIdAsync(timesheet.UserId).Result;
            var timeWorked = TimeSheetTimeSpent(timesheet);
            var amountMade = TimeSheetAmountMade(timesheet, timeWorked);


            var model = new SupervisorTimeSheetViewModel
            {
                StartDate = timesheet.StartDate,
                EndDate = timesheet.EndDate,
                Exempt = timesheet.ExemptFromOvertime,
                Approved = timesheet.Approved,
                TotalEarning = amountMade,
                TotalHours = timeWorked,
                DenialReason = timesheet.ReasonDenied ?? "Not Denied Yet",
                TimesheetId = timesheet.Id,
                User = timesheetUser.Email
            };


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisorReviewTimesheet(SupervisorTimeSheetViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Approved == ApprovalType.Denied)
                {
                    if (model.DenialReason != null)
                    {
                        var timesheet = _context.TimeSheets.Find(model.TimesheetId);
                        timesheet.Approved = model.Approved;
                        timesheet.ReasonDenied = model.DenialReason;
                        _context.TimeSheets.Update(timesheet);
                        _context.SaveChanges();
                    }
                    else
                    {
                        //Denial Reason is mandatory if the TimeSheet is denied
                        return View(model);
                    }
                }
                else
                {
                    var timesheet = _context.TimeSheets.Find(model.TimesheetId);
                    timesheet.Approved = model.Approved;
                    _context.TimeSheets.Update(timesheet);
                    _context.SaveChanges();
                    return View("Index");
                }
            }


            return RedirectToAction("SupervisorsPendingTimeSheets");
        }

        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult AllWeeklyReport()
        {
            var report = new List<WeeklyReportViewModel>();
            var timesheets = _context.TimeSheets.Where(x => x.Approved == ApprovalType.Approved);

            var weeks = timesheets.ToLookup(x => x.StartDate, x => x.EndDate);

            foreach (var week in weeks)
            {
                report.Add(new WeeklyReportViewModel
                {
                    StartDate = week.Key,
                    EndDate = week.First(),
                    TotalEarning = GetWeeklyEarnings(week.Key, week.First())

                });
            }

            return View(report);
        }
        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult GroupsWeekReport(DateTime startDate, DateTime endDate)
        {
            var timesheets = _context.TimeSheets.Where(x => x.StartDate == startDate && x.EndDate == endDate && x.User.SupervisorId != null).Include(x => x.User);
            var report = new List<GroupWeeklyReportViewModel>();
            var groups = timesheets.ToLookup(x => x.User.SupervisorId).OrderBy(x => x.Key);

            foreach (var group in groups)
            {
                var supervisor = "No supervisor";
                if (group.Key != null)
                {
                    supervisor = _userManager.FindByIdAsync(group.Key).Result.Email;
                }

                report.Add(new GroupWeeklyReportViewModel
                {
                    SupervisorId = group.Key,
                    Supervisor = supervisor,
                    StartDate = group.First().StartDate,
                    EndDate = group.First().EndDate,
                    TotalEarning = GetWeeklyGroupEarnings(group.First().StartDate, group.First().EndDate, group.Key)
                });
            }

            return View(report);
        }

        [HttpGet]
        public IActionResult GroupsTimeCards(DateTime startDate, DateTime endDate, string supervisorId)
        {
            var timesheets = _context.TimeSheets.Where(x => x.StartDate == startDate && x.EndDate == endDate).Include(x => x.User).ThenInclude(x => x.Supervisor).Where(x => x.User.SupervisorId == supervisorId);

            var groupsTimeCards = new List<PayrollTimeSheetViewModel>();

            foreach (var timesheet in timesheets)
            {
                var timeworked = TimeSheetTimeSpent(timesheet);
                var amountMade = TimeSheetAmountMade(timesheet, timeworked);

                groupsTimeCards.Add(new PayrollTimeSheetViewModel
                {
                    EndDate = timesheet.EndDate,
                    StartDate = timesheet.StartDate,
                    User = timesheet.User.Email,
                    Supervisor = timesheet.User.Supervisor != null ? timesheet.User.Supervisor.Email : "No supervisor",
                    Approved = timesheet.Approved,
                    TimesheetId = timesheet.Id,
                    ExemptFromOvertime = timesheet.ExemptFromOvertime,
                    TimeWorked = timeworked,
                    AmountMade = amountMade
                });
            }


            return View(groupsTimeCards);    
        }

        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult CheckForTimesheet(int id)
        {
            var timesheet = _context.TimeSheets.Include(x => x.User).Where(x => x.Id == id).First();
            var timeworked = TimeSheetTimeSpent(timesheet);
            var amountMade = TimeSheetAmountMade(timesheet, timeworked);

            var viewModel = new PayrollTimeSheetViewModel
            {
                User = timesheet.User.FirstName + " " + timesheet.User.LastName,
                AmountMade = amountMade,
                TimeWorked = timeworked,
                StartDate = timesheet.StartDate,
                EndDate = timesheet.EndDate
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Payroll")]
        public IActionResult AllPendingTimesheets()
        {
            var timesheets = _context.TimeSheets.Where(x => x.Approved == ApprovalType.Waiting && x.User.SupervisorId != null).Include(x => x.User).ThenInclude(x => x.Supervisor);

            var pendings = new List<PayrollTimeSheetViewModel>();

            foreach (var timesheet in timesheets)
            {
                var timeworked = TimeSheetTimeSpent(timesheet);
                var amountMade = TimeSheetTimeSpent(timesheet);

                pendings.Add(new PayrollTimeSheetViewModel
                {
                    EndDate = timesheet.EndDate,
                    StartDate = timesheet.StartDate,
                    User = timesheet.User.Email,
                    Supervisor = timesheet.User.Supervisor != null ? timesheet.User.Supervisor.Email : "No supervisor",
                    Approved = timesheet.Approved,
                    TimesheetId = timesheet.Id,
                    ExemptFromOvertime = timesheet.ExemptFromOvertime,
                    TimeWorked = timeworked,
                    AmountMade = amountMade
                });
            }


            return View(pendings);
        }

        private double GetWeeklyEarnings(DateTime startDate, DateTime endDate)
        {
            var weekTimesheet = _context.TimeSheets.Where(x => x.StartDate == startDate && x.EndDate == endDate);
            double earning = 0;

            foreach (var timesheet in weekTimesheet)
            {
                var timeWorked = TimeSheetTimeSpent(timesheet);
                var amountMade = TimeSheetAmountMade(timesheet, timeWorked);
                earning += amountMade;
            }

            return earning;
        }

        private double GetWeeklyGroupEarnings(DateTime startDate, DateTime endDate, string supervisorId)
        {
            var weekTimesheet = _context.TimeSheets.Where(x => x.StartDate == startDate && x.EndDate == endDate)
                .Include(x => x.User).Where(x => x.User.SupervisorId == supervisorId);

            double earning = 0;

            foreach (var timesheet in weekTimesheet)
            {
                var timeWorked = TimeSheetTimeSpent(timesheet);
                var amountMade = TimeSheetAmountMade(timesheet, timeWorked);
                earning += amountMade;
            }

            return earning;
        }

        //[Authorize(Roles = "Employee, Manager, HRManager, Administator")]
        public IActionResult TimeSheet()
        {
            
            
            return View();
        }
        
    }
}
