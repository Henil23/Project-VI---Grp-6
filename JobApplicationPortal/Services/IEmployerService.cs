using JobApplicationPortal.Models;

namespace JobApplicationPortal.Services
{
    public interface IEmployerService
    {
        Task CreateEmployerAsync(Employer employer);
        Task<bool> UpdateEmployerAsync(string email, Employer updatedEmployer);
        Task<bool> UpdateEmployerFieldAsync(string email, string fieldName, string newValue);
        Task<Employer> GetEmployerByEmailAsync(string email);
        Task<bool> DeleteEmployerAsync(string email, string password);
    }
}
