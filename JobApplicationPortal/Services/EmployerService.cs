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

        public async Task<bool> UpdateEmployerAsync(string email, Employer updatedEmployer)
        {
            return await _employerRepository.UpdateEmployerAsync(email, updatedEmployer);
        }

        public async Task<bool> UpdateEmployerFieldAsync(string email, string fieldName, string newValue)
        {
            return await _employerRepository.UpdateEmployerFieldAsync(email, fieldName, newValue);
        }

        public async Task<Employer> GetEmployerByEmailAsync(string email)
        {
            return await _employerRepository.GetEmployerByEmailAsync(email);
        }

        public async Task<bool> DeleteEmployerAsync(string email, string password)
        {
            return await _employerRepository.DeleteEmployerAsync(email, password);
        }
    }
}
