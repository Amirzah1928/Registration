using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using registration.Entities;
using registration.Interfaces;
using registration.Models;

namespace registration.Controllers
{
    [Authorize]
    public class PremiumController : Controller
    {

        private readonly IPremiumActivationService _premiumActivationService;


        public PremiumController(IPremiumActivationService premiumActivationService)
        {
            _premiumActivationService = premiumActivationService;
        }


        public IActionResult Plans()
        {
            var ispremiumclaim = User.FindFirst("IsPremium")?.Value;
            if (ispremiumclaim != "False")
                return NotFound();

            
            return View();
        }





        
        public IActionResult BronzePlan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BronzePlan(PremiumPlansViewModel model)
        {
            if (!ModelState.IsValid || model.PaymentCode != "1928")
            {
                ModelState.AddModelError("", "Check your inputs");
                return View(model);
            }

            var activationResult = _premiumActivationService.Active(1, model.UserName);

            if (activationResult == false)
            {
                ModelState.AddModelError("", "Username is invalid");
                return View(model);
            }

            return RedirectToAction("Logout", "Registration");

        }








        
        public IActionResult SilverPlan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SilverPlan(PremiumPlansViewModel model)
        {
            if (!ModelState.IsValid || model.PaymentCode != "1928")
            {
                ModelState.AddModelError("", "Check your inputs");
                return View(model);
            }

            var activationResult = _premiumActivationService.Active(2, model.UserName);

            if (activationResult == false)
            {
                ModelState.AddModelError("", "Username and Password is invalid");
                return View(model);
            }

            return RedirectToAction("Logout", "Registration");
        }






        
        public IActionResult GoldenPlan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GoldenPlan(PremiumPlansViewModel model)
        {
            if (!ModelState.IsValid || model.PaymentCode != "1928")
            {
                ModelState.AddModelError("", "Check your inputs");
                return View(model);
            }

            var activationResult = _premiumActivationService.Active(3, model.UserName);

            if (activationResult == false)
            {
                ModelState.AddModelError("", "Username and Password is invalid");
                return View(model);
            }

            return RedirectToAction("Logout", "Registration");
        }
    }
}
