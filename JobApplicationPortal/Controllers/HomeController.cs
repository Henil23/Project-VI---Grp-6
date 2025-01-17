using Microsoft.AspNetCore.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}