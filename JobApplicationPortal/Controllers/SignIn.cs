using Microsoft.AspNetCore.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class SignIn : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}