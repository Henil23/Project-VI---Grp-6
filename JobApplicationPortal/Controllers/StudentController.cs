using Microsoft.AspNetCore.Mvc;
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using System.Threading.Tasks;
using MongoDB.Bson;
using JobApplicationPortal.Repo;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Caching.Memory;

namespace JobApplicationPortal.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMemoryCache _memoryCache;

        // Constructor injection for IStudentService
        public StudentController(IStudentService studentService, IMemoryCache memoryCache)
        {
            _studentService = studentService;
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        // GET: Show sign-in form
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStudent()
        {
            string studentID = HttpContext.Session.GetString("studentID");

            if (string.IsNullOrEmpty(studentID))
            {
                TempData["ErrorMessage"] = "Session expired. Please sign in again.";
                return RedirectToAction("SignIn");
            }

            var previousStudentRecords = await _studentService.GetStudentByIdAsync(studentID);

            if (previousStudentRecords == null)
            {
                return View(); // Handle case where student doesn't exist
            }

            return View(previousStudentRecords);
        }

        [HttpPatch]
        [ValidateAntiForgeryToken]  // Ensure CSRF token is validated
        public async Task<IActionResult> UpdateStudent([FromBody] JsonPatchDocument<Student> studentPatch)
        {
            if (studentPatch == null)
                return BadRequest("Invalid patch request");

            string studentId = HttpContext.Session.GetString("studentID");
            if (string.IsNullOrEmpty(studentId))
                return BadRequest("Student ID not found in session.");

            // Retrieve the student from the service
            var student = await _studentService.GetStudentByIdAsync(studentId);
            if (student == null)
                return NotFound("Student not found");

            // Exclude the CSRF token from being patched
            var tokenPath = studentPatch.Operations.FirstOrDefault(op => op.path.Contains("__RequestVerificationToken"));
            if (tokenPath != null)
            {
                studentPatch.Operations.Remove(tokenPath);  // Remove CSRF token from the operations
            }

            // Apply the patch to the student object
            studentPatch.ApplyTo(student, ModelState);

            if (!TryValidateModel(student))
            {
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            // Update the student in the database
            await _studentService.UpdateStudentAsync(studentId, student);

            // Return the updated student object
            return Ok(student);
        }


        [HttpGet()]
        public IActionResult DeleteStudentForm()
        {
            return View();
        }

        // POST: Handle student form submission
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
            HttpContext.Session.SetString("studentID", student.StudentID);

            // Cache the student object (single student at a time)
            _memoryCache.Set("CachedStudentID", student.StudentID, new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5)));

            // Redirect to success page or student dashboard
            return RedirectToAction("Index", "Jobs");
        }

        // PUT: Modify/Update the Entire Student's Details
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStudentPut([FromBody] Student studentData)
        {
            if (studentData == null)
            {
                return BadRequest("Invalid student data.");
            }

            string studentId = HttpContext.Session.GetString("studentID");
            if (string.IsNullOrEmpty(studentId))
            {
                return BadRequest("Student ID not found in session.");
            }

            // Retrieve the student from the service
            var student = await _studentService.GetStudentByIdAsync(studentId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Replace all fields with the new data
            student.FirstName = studentData.FirstName;
            student.LastName = studentData.LastName;
            student.StudentEmail = studentData.StudentEmail;
            student.StudentDOB = studentData.StudentDOB;
            student.StudentAddress = studentData.StudentAddress;
            student.StudentCity = studentData.StudentCity;
            student.StudentCountry = studentData.StudentCountry;
            student.School = studentData.School;
            student.StudentPassword = studentData.StudentPassword;

            // Validate model
            if (!TryValidateModel(student))
            {
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            // Update student in the database
            await _studentService.UpdateStudentAsync(studentId, student);

            return Ok(student);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteStudent()
        {
            string? studentID = null;

            // Try getting student ID from cache
            if (_memoryCache.TryGetValue("CachedStudentID", out string cachedStudentID))
            {
                studentID = cachedStudentID;
            }

            // If student ID is not found in cache, fallback to session
            if (string.IsNullOrEmpty(studentID))
            {
                studentID = HttpContext.Session.GetString("studentID");
            }

            if (!string.IsNullOrEmpty(studentID))
            {
                // Remove from cache
                _memoryCache.Remove("CachedStudentID");

                // Proceed with database deletion
                var result = await _studentService.DeleteStudentAsync(studentID);

                if (result == null)
                {
                    return NotFound();
                }

                // Clear session data
                HttpContext.Session.Clear();

                return NoContent(); // 204 No Content
            }

            return NotFound("Student not found or not logged in");
        }

        //option method implemented
        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }


    }
}