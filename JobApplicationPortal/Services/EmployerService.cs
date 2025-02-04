using JobApplicationPortal.Models;
using JobApplicationPortal.Repo;
using MongoDB.Driver;

namespace JobApplicationPortal.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly EmployRepo _employerRepository;

        public EmployerService(EmployRepo employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task AddEmployerAsync(Employer employer)
        {
            await _employerRepository.AddEmployerAsync(employer);
        }

        public async Task UpdateEmployerAsync(string id, Employer employer)
        {
            await _employerRepository.UpdateEmployerAsync(id, employer);
        }

        public async Task<DeleteResult> DeleteEmployerAsync(string id)
        {
            var result = await _employerRepository.DeleteEmployerAsync(id);
            return result;
        }
    }
}
