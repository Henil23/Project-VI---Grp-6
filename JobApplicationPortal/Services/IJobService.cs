//namespace JobApplicationPortal.Services
//{
//    using JobApplication.Models;

//using JobApplicationPortal.Models;

//    namespace JobApplication.Services
//    {
//        public interface IJobService
//        {
//            Task<List<Job>> GetAllJobsAsync();
//            Task<Job> GetJobByIdAsync(string id);
//            Task CreateJobAsync(Job job);
//            Task UpdateJobAsync(string id, Job job);
//            Task DeleteJobAsync(string id);
//        }
//    }

//}
using JobApplicationPortal.Models;

namespace JobApplicationPortal.Services
{
    public interface IJobService
    {
        Task<List<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(string id);
        Task CreateJobAsync(Job job);
        Task UpdateJobAsync(string id, Job job);
        Task DeleteJobAsync(string id);
    }
}
