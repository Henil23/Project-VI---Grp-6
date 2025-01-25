using JobApplicationPortal.Models;

namespace JobApplicationPortal.Services
{
    public interface IStudentService
    {
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(string id, Student student);
        Task<bool>DeleteStudentAsync(string email, string password);
    }
}
