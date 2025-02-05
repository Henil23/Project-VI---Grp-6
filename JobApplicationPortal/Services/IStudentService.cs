using JobApplicationPortal.Models;
using MongoDB.Driver;

namespace JobApplicationPortal.Services
{
    public interface IStudentService
    {
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(string id, Student student);
        Task<DeleteResult> DeleteStudentAsync(string id);
    }
}