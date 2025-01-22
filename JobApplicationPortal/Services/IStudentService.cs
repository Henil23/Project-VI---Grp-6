using JobApplicationPortal.Models;

namespace JobApplicationPortal.Services
{
    public interface IStudentService
    {
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(string id, Student student);
        Task DeleteStudentAsync(string id);

    }
}
