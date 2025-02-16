using JobApplicationPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplicationPortal.Services
{
    public interface IJobService
    {
        Task<List<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(string id);
        Task CreateJobAsync(Job job);  // This method must be implemented
        Task UpdateJobAsync(string id, Job job);
        Task DeleteJobAsync(string id);
    }
}
