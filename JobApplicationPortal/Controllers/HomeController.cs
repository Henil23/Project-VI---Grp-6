using Microsoft.AspNetCore.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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