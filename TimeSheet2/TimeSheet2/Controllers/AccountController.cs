using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet2.EntityFramework;
using TimeSheet2.ViewModels.AccountViewModels;

namespace TimeSheet2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);


        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        /// <summary>
        /// Get a lists of all the users to be able to adminster them
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            AccountIndexViewModel users = new AccountIndexViewModel { Users = _userManager.Users };

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel newUser)
        {


            //Add in Roles
            if (ModelState.IsValid)
            {
                //Create the user
                var user = new ApplicationUser
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    UserName = newUser.Email,
                    HourlyWage = newUser.HourlyWage
                };

                var result = await _userManager.CreateAsync(user, newUser.Password);

                if (result.Succeeded)
                {
                    //Add to roles
                    if (newUser.Role != null)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user.UserName), newUser.Role);

                        if (roleResult.Succeeded)
                        {
                            ViewData["Successful"] = $"Created {newUser.Email} Successfully and add to roll";
                        }
                        else
                        {
                            ViewData["Successful"] = $"{newUser.Email} was not added to roll, but created succesfully";
                        }
                    }
                    else
                    {
                        ViewData["Successful"] = $"It's null for some reason";
                    }

                }
                else
                {

                    ViewData["Successful"] = $"{newUser.Email} profile was not added to the DB";
                }
            }
            else
            {

                ViewData["Successful"] = "Some information was not correct";
            }

            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            var supervisors = await _userManager.GetUsersInRoleAsync("Supervisor");
            var allRoles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(allRoles, "Name");
            ViewBag.Supervisors = new SelectList(supervisors.ToList(), "Id", "Email");
            EditUserViewModel viewModel;
            if (role.Any())
            {
                viewModel = new EditUserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = role.First(),
                    UserId = user.Id,
                    HourlyWage = user.HourlyWage
                };
            }
            else
            {
                viewModel = new EditUserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserId = user.Id,
                    HourlyWage = user.HourlyWage
                };
            }

            if (user.SupervisorId != null)
            {
                viewModel.SupervisorId = user.SupervisorId;
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            //I'm just going to apologize and let you know I feel bad for this. 
            var allRoles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(allRoles, "Name");
            var supervisors = await _userManager.GetUsersInRoleAsync("Supervisor");
            ViewBag.Supervisors = new SelectList(supervisors, "Id");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.Email = model.Email;
                user.HourlyWage = model.HourlyWage;
                var roles = await _userManager.GetRolesAsync(user);

                if (model.Password != null)
                {
                    //Remove then set the users password
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);
                }

                //User has a role, remove it before hand
                if (roles != null && model.Role != null)
                {
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    await _userManager.AddToRoleAsync(user, model.Role);
                }

                if (model.SupervisorId != null)
                {
                    user.SupervisorId = model.SupervisorId;
                }

                //Update the user now
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ViewData["Successful"] = "Successfully updated user";
                    return RedirectToAction("UserDetails", new { id = user.Id });
                }
                else
                {
                    ViewData["Successful"] = "Not succesful at updating user";
                }

            }
            else
            {
                ViewData["Successful"] = "Information is not typed correctly";
            }

            //I need to pass in a view
            return View(model);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);


            UserDetailViewModel viewModel = new UserDetailViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                HourlyWage = user.HourlyWage
            };

            var role = await _userManager.GetRolesAsync(user);
            viewModel.Role = role.Any() ? role.First() : "Not in a Role";


            return View(viewModel);

        }
    }
}