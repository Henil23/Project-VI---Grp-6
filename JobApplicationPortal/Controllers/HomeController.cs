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
        public IActionResult AddJob()
        { 
            return View(); 
        }

    }
}