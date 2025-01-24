using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using registration.AccountService;
using registration.Entities;
using registration.Interfaces;
using registration.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace registration.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserPasswordService _userPasswordService;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public RegistrationController(ApplicationDbcontext dbcontext, IUserRegistrationService userRegistrationService, IUserPasswordService userPasswordService, IUserAuthenticationService userAuthenticationService)
        {
            _userRegistrationService = userRegistrationService;
            _dbcontext = dbcontext;
            _userPasswordService = userPasswordService;
            _userAuthenticationService = userAuthenticationService;
        }




        public IActionResult Index()
        {
            return View(_dbcontext.Users.ToList());
        }






        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userRegistrationService.RegisterUser(model);
                if (result.Success == true)
                {
                    ViewBag.Message = result.Message;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Signup error message", result.Message);
                    return View(model);
                }
            }
            return View(model);
        }







        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValidUser = _userAuthenticationService.SignInAsync(model.UserName, model.Password);

                if (isValidUser.Result)
                {
                    // Handle login (sign-in) logic here
                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }






        public ActionResult Logout()
        {
            Task<bool> Taskresult = _userAuthenticationService.SignOutAsync();
            return (Taskresult.Result) ? RedirectToAction("Index") : View();
        }





        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}