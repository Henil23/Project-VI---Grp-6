using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class EmployerController : Controller
    {
        private readonly IEmployerService _employerService;
        public EmployerController(IEmployerService employerService)
        {
            _employerService = employerService;
        }
        public IActionResult EmployerSignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployerSignUp(Employer e)
        {
            if (ModelState.IsValid)
            {
                await _employerService.AddEmployerAsync(e);

                // store employeer company name
                HttpContext.Session.SetString("employerCompany", e.CompanyName);

                // transfering employer information to get their associated job postings
                return RedirectToAction("JobListings", "Home", new { employerId = e.EmployerID });

            }
            else
            {
                return View("EmployerSignUp", e);
            }
        }
    }
}
