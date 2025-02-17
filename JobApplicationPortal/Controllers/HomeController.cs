using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;

namespace JobApplicationPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Retrieve session data
            string? firstName = HttpContext.Session.GetString("FirstName");
            string? isSignedIn = HttpContext.Session.GetString("IsSignedIn");

            // Pass session data to the view
            ViewData["FirstName"] = firstName;
            ViewData["IsSignedIn"] = isSignedIn;

            // Return the view, passing the student object
            return View();
        }
        public IActionResult JobListings()
        {
            return View();
        }
        public IActionResult PostJob()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitContactForm(string name, string email, string message)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                TempData["ErrorMessage"] = "All fields are required.";
                return RedirectToAction("ContactUs"); // Reloads form with error message
            }

            // Simulate storing the message or sending an email
            Console.WriteLine($"Contact request from {name} ({email}): {message}");

            // Store success message in session
            TempData["SuccessMessage"] = "Your request has been sent successfully!";

            // Redirect to the Home page
            return RedirectToAction("Index");
        }


        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }

    }
}