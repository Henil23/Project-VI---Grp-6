using JobApplicationPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplicationPortal.Services
{
    public interface IJobService
    {
        Task<List<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(string jobID);
        Task CreateJobAsync(Job job);
        Task UpdateJobAsync(string jobID, Job job);
        Task DeleteJobAsync(string jobID);
    }
}
