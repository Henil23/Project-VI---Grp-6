using Microsoft.AspNetCore.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View("EmployeeSignIn");
        }
    }
}
