using JobApplicationPortal.Models;
using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value;
        var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value;

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
    // Collection for student data
    public IMongoCollection<Student> Students => _database.GetCollection<Student>("Students");

    //Collection for employer data
    public IMongoCollection<Student> Employer => _database.GetCollection<Student>("Employer");

    //Collection for job data
    public IMongoCollection<Job> Jobs => _database.GetCollection<Job>("Jobs");
}
