using JobApplicationPortal.Models;
using MongoDB.Driver;


namespace JobApplicationPortal.Repo
{
    public class StudentRepo
    {
        private readonly IMongoCollection<Student> _studentCollection;

        public StudentRepo(MongoDbContext context)
        {
            _studentCollection = context.GetCollection<Student>("Students");
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentCollection.InsertOneAsync(student);
        }

        public async Task UpdateStudentAsync(string id, Student student)
        {
            await _studentCollection.ReplaceOneAsync(s => s.StudentID == id, student);
        }

        public async Task DeleteStudentAsync(string id)
        {
            await _studentCollection.DeleteOneAsync(s => s.StudentID == id);
        }
    }
}
