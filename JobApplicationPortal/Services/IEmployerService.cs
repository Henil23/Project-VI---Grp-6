using JobApplicationPortal.Models;

namespace JobApplicationPortal.Services
{
    public interface IEmployerService
    {
        Task CreateEmployerAsync(Employer employer);
        Task UpdateEmployerAsync(string id, Employer employer);
        Task<bool> DeleteEmployerAsync(string email, string password);
        
        //Task CreateEmployerAsync(Employer employer);
    }
}
