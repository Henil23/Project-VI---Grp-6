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

        public async Task<bool> UpdateEmployerAsync(string email, Employer updatedEmployer)
        {
            var filter = Builders<Employer>.Filter.Eq(e => e.EmployerEmail, email);
            var result = await _employerCollection.ReplaceOneAsync(filter, updatedEmployer);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> UpdateEmployerFieldAsync(string email, string fieldName, string newValue)
        {
            var filter = Builders<Employer>.Filter.Eq(e => e.EmployerEmail, email);
            var update = Builders<Employer>.Update.Set(fieldName, newValue);
            var result = await _employerCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<Employer> GetEmployerByEmailAsync(string email)
        {
            return await _employerCollection.Find(e => e.EmployerEmail == email).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteEmployerAsync(string email, string password)
        {
            var result = await _employerCollection.DeleteOneAsync(s => s.EmployerEmail == email && s.EmployerPassword == password);
            return result.DeletedCount > 0;
        }
    }
}
