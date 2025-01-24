using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationPortal.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StudentID { get; set; }

        [BsonElement("studentPassword")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be over 6 characters long.")]
        public string? StudentPassword { get; set; }

        [BsonElement("studentFirstName")]
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }

        [BsonElement("studentLastName")]
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        [BsonElement("studentEmail")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? StudentEmail { get; set; }

        [BsonElement("studentDOB")]
        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime? StudentDOB { get; set; }

        [BsonElement("studentAddress")]
        [Required(ErrorMessage = "Address is required.")]
        public string? StudentAddress { get; set; }

        [BsonElement("studentCity")]
        [Required(ErrorMessage = "City is required.")]
        public string? StudentCity { get; set; }

        [BsonElement("studentCountry")]
        [Required(ErrorMessage = "Country is required.")]
        public string? StudentCountry { get; set; }

        [BsonElement("studentEd")]
        [Required(ErrorMessage = "School is required.")]
        public string? School { get; set; }

        // Parameterless constructor
        public Student() { }

        // Constructor
        public Student(string id, string password, string firstName, string lastName, string email, DateTime dob,
            string address, string city, string country, string school)
        {
            StudentID = id;
            StudentPassword = password;
            FirstName = firstName;
            LastName = lastName;
            StudentEmail = email;
            StudentDOB = dob;
            StudentAddress = address;
            StudentCity = city;
            StudentCountry = country;
            School = school;
        }
    }
}
