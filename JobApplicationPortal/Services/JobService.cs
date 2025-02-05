using JobApplicationPortal.Models;
using JobApplicationPortal.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplicationPortal.Services
{
    public class JobService : IJobService
    {
        private readonly JobRepo _jobRepository;

        public JobService(JobRepo jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _jobRepository.GetAllJobsAsync();
        }

        public async Task<Job> GetJobByIdAsync(string jobID)
        {
            return await _jobRepository.GetJobByIdAsync(jobID);
        }

        public async Task CreateJobAsync(Job job)
        {
            await _jobRepository.AddJobAsync(job);
        }

        public async Task UpdateJobAsync(string jobID, Job job)
        {
            await _jobRepository.UpdateJobAsync(jobID, job);
        }

        public async Task DeleteJobAsync(string jobID)
        {
            await _jobRepository.DeleteJobAsync(jobID);
        }
    }
}
