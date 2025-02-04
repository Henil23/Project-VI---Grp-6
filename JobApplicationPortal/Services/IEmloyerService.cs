using JobApplicationPortal.Models;
using MongoDB.Driver;

namespace JobApplicationPortal.Services
{
    public interface IEmployerService
    {
        public Task AddEmployerAsync(Employer employer);
        public Task UpdateEmployerAsync(string id, Employer employer);
        public Task<DeleteResult> DeleteEmployerAsync(string id);
    }
}
