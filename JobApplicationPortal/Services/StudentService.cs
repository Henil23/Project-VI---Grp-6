using Amazon.Runtime.Internal.Auth;
using JobApplicationPortal.Models;
using JobApplicationPortal.Repo;
using MongoDB.Driver;

namespace JobApplicationPortal.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepo _studentRepository;

        public StudentService(StudentRepo studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task CreateStudentAsync(Student student)
        {
            await _studentRepository.AddStudentAsync(student);
        }

        public async Task UpdateStudentAsync(string id, Student student)
        {
            await _studentRepository.UpdateStudentAsync(id, student);
        }

        public async Task<DeleteResult> DeleteStudentAsync(string id)
        {
            var result = await _studentRepository.DeleteStudentAsync(id);
            return result;
        }

    }
}
