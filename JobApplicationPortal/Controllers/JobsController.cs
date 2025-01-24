//using JobApplicationPortal.Services;
//using JobApplicationPortal.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace JobApplicationPortal.Controllers
//{
//    public class JobsController : Controller
//    {
//        private readonly IJobService _jobService;

//        public JobsController(IJobService jobService)
//        {
//            _jobService = jobService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var jobs = await _jobService.GetAllJobsAsync();

//            if (jobs == null)
//            {
//                // Log or break here to check the issue
//                Console.WriteLine("No jobs found.");
//            }

//            return View(jobs);
//        }

//        public async Task<IActionResult> Details(string id)
//        {
//            var job = await _jobService.GetJobByIdAsync(id);
//            if (job == null) return NotFound();
//            return View(job);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(Job job)
//        {
//            if (!ModelState.IsValid) return View(job);
//            await _jobService.CreateJobAsync(job);
//            return RedirectToAction(nameof(Index));
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(string id, Job job)
//        {
//            if (!ModelState.IsValid) return View(job);
//            await _jobService.UpdateJobAsync(id, job);
//            return RedirectToAction(nameof(Index));
//        }

//        [HttpPost]
//        public async Task<IActionResult> Delete(string id)
//        {
//            await _jobService.DeleteJobAsync(id);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // Fetch all jobs and pass to the view
        public async Task<IActionResult> Index()
        {
            var jobs = await _jobService.GetAllJobsAsync(); // Fetch all jobs
            return View(jobs); // Pass the jobs list to the view
        }

        // Display job details
        public async Task<IActionResult> Details(string id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound(); // Handle missing job scenario
            }
            return View(job); // Pass the job to the Details view
        }
    }

}
