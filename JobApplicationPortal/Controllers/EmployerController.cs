using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace JobApplicationPortal.Controllers
{
    public class EmployerController : Controller
    {
        private readonly IEmployerService _employerService;
        private readonly IMemoryCache _memoryCache;

        public EmployerController(IEmployerService employerService, IMemoryCache memoryCache)
        {
            _employerService = employerService;
            _memoryCache = memoryCache;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        //NEEDS TO BE A DELETE REQUEST
        [HttpGet]
        public IActionResult DeleteEmployerInfo()
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
            HttpContext.Session.SetString("Email", employer.EmployerEmail);
            HttpContext.Session.SetString("Password", employer.EmployerPassword);

            // caching firstname of employer for 5 minutes
            _memoryCache.Set("CachedEmployerEmail", employer.EmployerEmail, new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5)));

            return View("~/Views/Home/PostJob.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployer()
        {
            string? email = null;
            // Try getting student ID from cache
            if (_memoryCache.TryGetValue("CachedEmployerEmail", out string cachedEmail))
            {
                email = cachedEmail;
            }
            else return NotFound();

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
                // return NoContent();
            }

            string email = _memoryCache.Get("CachedEmployerEmail").ToString();
            if (!string.IsNullOrEmpty(email))
            {
                email = HttpContext.Session.Get("Email").ToString();
            }
            var result = await _employerService.UpdateEmployerAsync(email, updatedEmployer);

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

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployer(/*string email, *//*string password*/)
        {

            string? employerEmail = string.Empty;
            string? employerPassword = HttpContext.Session.GetString("Password");
            if (ModelState.IsValid)
            {
                if (_memoryCache.TryGetValue("CachedEmployerEmail", out string cachedEmail))
                {
                    employerEmail = cachedEmail;
                }
                else
                {
                    employerEmail = HttpContext.Session.Get("Password").ToString();
                }
                var isDeleted = await _employerService.DeleteEmployerAsync(employerEmail, employerPassword);

                if (isDeleted)
                {
                    HttpContext.Session.Clear();
                    return Json(new { success = true, message = "Employer account has been deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Error: No employer found with the provided email." });
                }
            }
            else
            {
                return NoContent();
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
