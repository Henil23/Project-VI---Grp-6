using JobApplicationPortal.Models;
using MongoDB.Driver;

namespace JobApplicationPortal.Repo
{
    public class EmployRepo
    {
        private readonly IMongoCollection<Employer> _studentCollection;

        public EmployRepo(MongoDbContext context)
        {
            _studentCollection = context.GetCollection<Employer>("Employers");
        }

        public async Task AddEmployerAsync(Employer employer)
        {
            await _studentCollection.InsertOneAsync(employer);
        }

        public async Task UpdateEmployerAsync(string id, Employer employer)
        {
            await _studentCollection.ReplaceOneAsync(e => e.EmployerID == id, employer);
        }

        public async Task<DeleteResult> DeleteEmployerAsync(string id)
        {
            // Delete the student by matching both email and password
            var results = await _studentCollection.DeleteOneAsync(e => e.EmployerID == id);
            return results;
        }
    }
}
