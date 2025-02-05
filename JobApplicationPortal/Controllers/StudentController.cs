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

        [HttpGet()]
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
            HttpContext.Session.SetString("studentID", student.StudentID);

            // Redirect to success page or student dashboard
            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent()
        {
            string? studentID = HttpContext.Session.GetString("studentID");

            if (studentID != null)
            {

                var result = await _studentService.DeleteStudentAsync(studentID);

                if (result == null)
                {
                    return NotFound();
                }

                // removes all the student info
                HttpContext.Session.Remove("studentID");
                HttpContext.Session.Remove("FirstName");
                HttpContext.Session.Remove("IsSignedIn");

                // Redirect to homepage
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return NotFound();
            }
        }


    }
}