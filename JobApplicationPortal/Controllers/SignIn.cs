using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using MongoDB.Bson;


namespace JobApplicationPortal.Controllers
{
    public class SignIn : Controller
    {
        private readonly IStudentService _studentService;

        public SignIn(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            // Retrieve form data
            var firstName = Request.Form["firstName"];
            var lastName = Request.Form["lastName"];
            var email = Request.Form["email"];
            var dobString = Request.Form["DOB"];
            var address = Request.Form["address"];
            var city = Request.Form["city"];
            var country = Request.Form["country"];
            var selectedSchool = Request.Form["post-secondary"];
            var password = Request.Form["studentPassword"];

            Console.WriteLine("Retreives all fields at the points");

            // Parse DateOnly
            DateOnly? dob = null;
            if (DateOnly.TryParse(dobString, out var parsedDOB))
            {
                dob = parsedDOB;
            }

            // Generate a new ID
            string id = ObjectId.GenerateNewId().ToString();

            // Create a new Student object
            Student student = new Student(
                id,
                password,
                firstName,
                lastName,
                email,
                dob,
                address,
                city,
                country,
                selectedSchool
            );

            Console.WriteLine("Initializes student object");

            // Call the service to process the student object (e.g., save to database)
            _studentService.CreateStudentAsync(student);

            Console.WriteLine("Stores information to DB");

            // Redirect or show success message
            return RedirectToAction("Index");
        }
    }
}