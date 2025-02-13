using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using registration.Entities;
using registration.Interfaces;
using registration.Models;


namespace registration.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IComfirmPasswordService _resetPasswordService;
        public RegistrationController(IComfirmPasswordService resetPasswordService, ApplicationDbcontext dbcontext, IUserRegistrationService userRegistrationService,
            IUserPasswordService userPasswordService, IUserAuthenticationService userAuthenticationService)
        {
            _userRegistrationService = userRegistrationService;
            _dbcontext = dbcontext;
            _userAuthenticationService = userAuthenticationService;
            _resetPasswordService = resetPasswordService;
        }


        public IActionResult Index()
        {
            return View(_dbcontext.Users.ToList());
        }





        
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            return View();
        }


        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _userRegistrationService.RegisterUser(model);
            if (result.Success == true)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            ModelState.AddModelError("Signup error message", result.Message);
            return View(model);
        }








        
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var isValidUser = _userAuthenticationService.SignInAsync(model.UserName, model.Password);

            if (isValidUser.Result)
            {
                // Handle login (sign-in) logic here
                return RedirectToAction("SecurePage");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }









        //ForgetPassword Service Actions

        [Authorize]
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ForgetPassword(ComfirmPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var result = _resetPasswordService.FindUserbyEmail(model.Email);

            if (result == null)
            {
                ModelState.AddModelError("", "Email not found");
                return View(model);
            }

            HttpContext.Session.SetString("Email",model.Email);
            //HttpContext.Session.SetString("User", result.Password);
            HttpContext.Session.SetString("OtpCode", _resetPasswordService.CodeGenrator());
           
            return RedirectToAction("ComfirmPassword");
        }








        [Authorize]
        [HttpGet]
        public IActionResult ComfirmPassword()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            TempData["otpcode"] = HttpContext.Session.GetString("OtpCode");
            return View();
        }

        [HttpPost]
        public IActionResult ComfirmPassword(string otp)
        {
            var otpCode = HttpContext.Session.GetString("OtpCode");
            if (otp == otpCode)
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ModelState.AddModelError("", "Code was wrong");
            }
            return RedirectToAction("ForgetPassword");
        }





        [Authorize]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userEmail = HttpContext.Session.GetString("Email");
            var resetResult = _resetPasswordService.ResetPasswordDb(model.Password, userEmail);

            return (resetResult == true) ? RedirectToAction("Login") : View(model);

        }












        [Authorize]
        public ActionResult Logout()
        {
            var taskresult = _userAuthenticationService.SignOutAsync();
            return (taskresult.Result) ? RedirectToAction("Index") : View();
        }





        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }


    }
}