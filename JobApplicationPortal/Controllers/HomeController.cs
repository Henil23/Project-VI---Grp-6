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

    }
}