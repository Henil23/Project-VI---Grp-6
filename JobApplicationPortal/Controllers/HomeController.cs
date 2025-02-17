using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;

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
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitContactForm(string name, string email, string message)
        {
            // Process the form data
            Console.WriteLine($"Name: {name}, Email: {email}, Message: {message}");

            // Redirect to the job listing page after submission
            TempData["Message"] = "Thank you for contacting us! We will get back to you soon.";
            return RedirectToAction("JobListings", "Home"); // Redirect to the JobListing page
        }

      
    }
}