using Microsoft.AspNetCore.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class AddJobController : Controller
    {
        public IActionResult Index()
        {
            return View("AddJob");
        }
    }
}
