using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using System.Threading.Tasks;
using MongoDB.Bson;
using JobApplicationPortal.Repo;

namespace JobApplicationPortal.Controllers
{
    public class EmployerController : Controller
    {
        private readonly IEmployerService _employerService;

        // Constructor injection for IStudentService
        public EmployerController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        // GET: Show sign-in form
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteEmployerForm()
        {
            return View("DeleteEmployerInfo");
        }

        // POST: Handle student form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitEmployerForm(Employer employer)
        {
            //student.StudentID = ObjectId.GenerateNewId().ToString();
            // Model validation check
            if (!ModelState.IsValid)
            {
                return View("SignIn", employer); // Return to form with validation errors
            }

            // employer is signed in
            employer.IsSignedIn = true;

            // Use the service to save the student data
            await _employerService.CreateEmployerAsync(employer);

            // Store data in session
            HttpContext.Session.SetString("FirstName", employer.FirstName);
            HttpContext.Session.SetString("IsSignedIn", employer.IsSignedIn.ToString());

            // Redirect to success page or student dashboard
            return RedirectToAction("PostJob", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployer(string email, string password)
        {
            if (ModelState.IsValid)
            {
                // Try to delete the student by email and password
                var isDeleted = await _employerService.DeleteEmployerAsync(email, password);

                if (isDeleted)
                {
                    // If deletion was successful
                    ViewBag.Message = "Employer account has been deleted successfully.";
                }
                else
                {
                    // If student was not found or not deleted
                    ViewBag.Message = "Error: No employer found with the provided email and password.";
                }

                // Return the view with a message
                return View("DeleteEmployerInfo");
            }
            else
            {
                // If the model is invalid
                ViewBag.Message = "Error: Employer information is incomplete or invalid.";
                return View("DeleteEmployerInfo");
            }
        }

    }
}
