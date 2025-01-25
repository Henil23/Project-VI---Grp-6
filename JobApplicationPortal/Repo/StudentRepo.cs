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
            Console.WriteLine($"Inserting student: {student.FirstName} {student.LastName}");
        }

        public async Task UpdateStudentAsync(string id, Student student)
        {
            await _studentCollection.ReplaceOneAsync(s => s.StudentID == id, student);
        }

        public async Task<bool>DeleteStudentAsync(string email, string password)
        {
            // Delete the student by matching both email and password
            var result = await _studentCollection.DeleteOneAsync(s => s.StudentEmail == email && s.StudentPassword == password);
            return result.DeletedCount > 0;
        }

    }
}
