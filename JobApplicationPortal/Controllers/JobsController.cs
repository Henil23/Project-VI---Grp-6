using JobApplicationPortal.Models;
using JobApplicationPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JobApplicationPortal.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // Fetch all jobs and pass them to JobListings view
        public async Task<IActionResult> Index()
        {
            var jobs = await _jobService.GetAllJobsAsync();  // Fetch jobs from DB

            if (jobs == null || jobs.Count == 0)
            {
                Console.WriteLine("No jobs found.");
            }

            return View("~/Views/Home/JobListings.cshtml", jobs);  // Pass jobs to the view
        }


        // Display Post Job form
        public IActionResult PostJob()
        {
            return View();
        }

        // Handle Post Job submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostJob(Job job)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _jobService.CreateJobAsync(job);  // Ensure this adds to MongoDB
                    Console.WriteLine("Job successfully posted.");

                    // Redirect to the JobListings page after posting
                    return RedirectToAction("Index");  // Index will load JobListings.cshtml
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error posting job: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while posting the job.");
                }
            }
            return View(job);
        }


        // Display job details
        public async Task<IActionResult> Details(string jobID)
        {
            if (string.IsNullOrEmpty(jobID))
            {
                return BadRequest("Job ID is required.");
            }

            var job = await _jobService.GetJobByIdAsync(jobID);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // Handle Edit Job submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string jobID, Job job)
        {
            if (string.IsNullOrEmpty(jobID) || jobID != job.JobID)
            {
                return BadRequest("Job ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                await _jobService.UpdateJobAsync(jobID, job);
                Console.WriteLine("Job successfully updated.");
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // Handle Delete Job
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string jobID)
        {
            if (string.IsNullOrEmpty(jobID))
            {
                return BadRequest("Job ID is required.");
            }

            await _jobService.DeleteJobAsync(jobID);
            Console.WriteLine("Job successfully deleted.");
            return RedirectToAction("Index");
        }
    }
}
