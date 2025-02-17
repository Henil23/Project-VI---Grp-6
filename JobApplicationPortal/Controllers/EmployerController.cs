using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using System.Threading.Tasks;

namespace JobApplicationPortal.Controllers
{
    public class EmployerController : Controller
    {
        private readonly IEmployerService _employerService;

        public EmployerController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        //NEEDS TO BE A DELETE REQUEST
        [HttpGet]
        public IActionResult DeleteEmployerForm()
        {
            return View("DeleteEmployerInfo");
        }
        [HttpGet]
        public IActionResult SubmitEmployerForm()
        {
            return View ("SubmitEmployerForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitEmployerForm(Employer employer)
        {
            if (!ModelState.IsValid)
            {
                return View("SignIn", employer);
            }

            employer.IsSignedIn = true;
            await _employerService.CreateEmployerAsync(employer);

            HttpContext.Session.SetString("FirstName", employer.FirstName);
            HttpContext.Session.SetString("IsSignedIn", employer.IsSignedIn.ToString());

            return View("~/Views/Home/PostJob.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployer(string email)
        {
            var employer = await _employerService.GetEmployerByEmailAsync(email);
            if (employer == null)
            {
                return NotFound("Employer not found.");
            }
            return View("EditEmployer", employer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployer(Employer updatedEmployer)
        {
            if (!ModelState.IsValid)
            {
                return View("EditEmployer", updatedEmployer);
            }

            var result = await _employerService.UpdateEmployerAsync(updatedEmployer.EmployerEmail, updatedEmployer);

            if (result)
            {
                ViewBag.Message = "Employer information updated successfully.";
                return RedirectToAction("PostJob", "Jobs");
            }
            else
            {
                ViewBag.Message = "Error updating employer information.";
                return View("EditEmployer", updatedEmployer);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCompanyName(string email, string newCompanyName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newCompanyName))
            {
                return BadRequest("Email and new company name are required.");
            }

            var result = await _employerService.UpdateEmployerFieldAsync(email, "CompanyName", newCompanyName);

            if (result)
            {
                return Ok("Company name updated successfully.");
            }
            else
            {
                return NotFound("Employer not found or update failed.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployer(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var isDeleted = await _employerService.DeleteEmployerAsync(email, password);

                if (isDeleted)
                {
                    ViewBag.Message = "Employer account has been deleted successfully.";
                }
                else
                {
                    ViewBag.Message = "Error: No employer found with the provided email and password.";
                }

                return View("DeleteEmployerInfo");
            }
            else
            {
                ViewBag.Message = "Error: Employer information is incomplete or invalid.";
                return View("DeleteEmployerInfo");
            }
        }

        //Option implemented
        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }
    }
}
