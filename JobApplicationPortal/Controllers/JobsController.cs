using JobApplicationPortal.Services;
//using JobPortal.Models;
using JobPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobService.GetAllJobsAsync();

            if (jobs == null)
            {
                // Log or break here to check the issue
                Console.WriteLine("No jobs found.");
            }

            return View(jobs);
        }

        public async Task<IActionResult> Details(string id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null) return NotFound();
            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            if (!ModelState.IsValid) return View(job);
            await _jobService.CreateJobAsync(job);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Job job)
        {
            if (!ModelState.IsValid) return View(job);
            await _jobService.UpdateJobAsync(id, job);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _jobService.DeleteJobAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
