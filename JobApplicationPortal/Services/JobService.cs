using JobApplicationPortal.Services;
//using JobApplicationPortal.Models;
using JobPortal.Repositories;

namespace JobPortal.Services
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

        public async Task<Job> GetJobByIdAsync(string id)
        {
            return await _jobRepository.GetJobByIdAsync(id);
        }

        public async Task CreateJobAsync(Job job)
        {
            await _jobRepository.AddJobAsync(job);
        }

        public async Task UpdateJobAsync(string id, Job job)
        {
            await _jobRepository.UpdateJobAsync(id, job);
        }

        public async Task DeleteJobAsync(string id)
        {
            await _jobRepository.DeleteJobAsync(id);
        }
    }
}
