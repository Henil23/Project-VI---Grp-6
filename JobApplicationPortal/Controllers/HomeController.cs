using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;

namespace JobApplicationPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(Student student)
        {
            // If a valid Student object is provided and the student is signed in
            if (student != null && student.IsSignedIn)
            {
                // Pass the student's details to ViewData for display
                ViewData["FirstName"] = student.FirstName;
                ViewData["SignedIn"] = true; // Explicitly mark as signed in
                ViewData["Student"] = student;
            }
            else
            {
                // Fallback to TempData if no valid Student object is provided
                ViewData["FirstName"] = TempData["FirstName"];
                ViewData["SignedIn"] = TempData["SignedIn"] ?? false;
            }

            // Return the view, passing the student object
            return View(student);

        }
        public IActionResult JobListings()
        {
            return View();
        }

    }
}