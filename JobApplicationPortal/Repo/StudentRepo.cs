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

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            return await _studentCollection.Find(s => s.StudentID == id).FirstOrDefaultAsync();
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

        public async Task<DeleteResult> DeleteStudentAsync(string id)
        {
            // Delete the student by matching both email and password
            var results = await _studentCollection.DeleteOneAsync(s => s.StudentID == id);
            return results;
        }

    }
}
