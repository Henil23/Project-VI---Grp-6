using JobApplicationPortal.Models;
using MongoDB.Driver;


namespace JobApplicationPortal.Repo
{
    public class EmployerRepo
    {
        private readonly IMongoCollection<Employer> _employerCollection;

        public EmployerRepo(MongoDbContext context)
        {
            _employerCollection = context.GetCollection<Employer>("Employer");
        }

        public async Task AddEmployerAsync(Employer employer)
        {
            await _employerCollection.InsertOneAsync(employer);
            Console.WriteLine($"Inserting employer: {employer.FirstName} {employer.LastName}");
        }

        public async Task UpdateEmployerAsync(string id, Employer employer)
        {
            await _employerCollection.ReplaceOneAsync(s => s.EmployerID == id, employer);
        }

        public async Task<bool> DeleteEmployerAsync(string email, string password)
        {
            // Delete the student by matching both email and password
            var result = await _employerCollection.DeleteOneAsync(s => s.EmployerEmail == email && s.EmployerPassword == password);
            return result.DeletedCount > 0;
        }

    }
}
