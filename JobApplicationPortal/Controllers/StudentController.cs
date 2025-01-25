using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using System.Threading.Tasks;
using MongoDB.Bson;
using JobApplicationPortal.Repo;

namespace JobApplicationPortal.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        // Constructor injection for IStudentService
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Show sign-in form
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteStudentForm()
        {
            return View("DeleteStudentInfo");
        }

        // POST: Handle student form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitStudentForm(Student student)
        {
            //student.StudentID = ObjectId.GenerateNewId().ToString();
            // Model validation check
            if (!ModelState.IsValid)
            {
                return View("SignIn", student); // Return to form with validation errors
            }

            // student is signed in
            student.IsSignedIn = true;

            // Use the service to save the student data
            await _studentService.CreateStudentAsync(student);

            // Store data in session
            HttpContext.Session.SetString("FirstName", student.FirstName);
            HttpContext.Session.SetString("IsSignedIn", student.IsSignedIn.ToString());

            // Redirect to success page or student dashboard
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudent(string email, string password)
        {
            if (ModelState.IsValid)
            {
                // Try to delete the student by email and password
                var isDeleted = await _studentService.DeleteStudentAsync(email, password);

                if (isDeleted)
                {
                    // If deletion was successful
                    ViewBag.Message = "Student account has been deleted successfully.";
                }
                else
                {
                    // If student was not found or not deleted
                    ViewBag.Message = "Error: No student found with the provided email and password.";
                }

                // Return the view with a message
                return View("DeleteStudentInfo");
            }
            else
            {
                // If the model is invalid
                ViewBag.Message = "Error: Student information is incomplete or invalid.";
                return View("DeleteStudentInfo");
            }
        }

    }
}
