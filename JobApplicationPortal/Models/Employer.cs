
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationPortal.Models
{
    public class Employer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? EmployerID { get; set; }

        [BsonElement("EmployerPassword")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 32 characters long.")]
        public string? EmployerPassword { get; set; }

        [BsonElement("EmployerFirstName")]
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }

        [BsonElement("EmployerLastName")]
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        [BsonElement("EmployerEmail")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? EmployerEmail { get; set; }

        [BsonElement("CompanyName")]
        [Required(ErrorMessage = "Company Name is required.")]
        public string? CompanyName { get; set; }

        public bool IsSignedIn { get; set; }

        // List of jobs associated with the employer
        public List<Job>? PostedJobs { get; set; }

        // Parameterless constructor
        public Employer()
        {
            PostedJobs = new List<Job>();
        }

        // Constructor with parameters
        public Employer(string id, string password, string firstName, string lastName, string email, string companyName, bool isSignedIn)
        {
            EmployerID = id;
            EmployerPassword = password;
            FirstName = firstName;
            LastName = lastName;
            EmployerEmail = email;
            CompanyName = companyName;
            IsSignedIn = isSignedIn;
            PostedJobs = new List<Job>();
        }
    }
}
