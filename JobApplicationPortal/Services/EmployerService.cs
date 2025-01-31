using Amazon.Runtime.Internal.Auth;
using JobApplicationPortal.Models;
using JobApplicationPortal.Repo;

namespace JobApplicationPortal.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly EmployerRepo _employerRepository;

        public EmployerService(EmployerRepo employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task CreateEmployerAsync(Employer employer)
        {
            await _employerRepository.AddEmployerAsync(employer);
        }

        public async Task UpdateEmployerAsync(string id, Employer employer)
        {
            await _employerRepository.UpdateEmployerAsync(id, employer);
        }

        public async Task<bool> DeleteEmployerAsync(string email, string password) => await _employerRepository.DeleteEmployerAsync(email, password);
    }
}
