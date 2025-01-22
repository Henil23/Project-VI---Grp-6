using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace JobApplicationPortal.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string StudentID { get; set; }

        [BsonElement("studentPassword")]
        public string StudentPassword { get; set; }

        [BsonElement("studentFirstName")]
        public string? FirstName { get; set; }

        [BsonElement("studentLastName")]
        public string? LastName { get; set; }

        [BsonElement("studentEmail")]
        public string? StudentEmail { get; set; }

        [BsonElement("studentDOB")]
        public DateOnly? StudentDOB { get; set; }

        [BsonElement("studentAddress")]
        public string? StudentAddress { get; set; }

        [BsonElement("studentCity")]
        public string? StudentCity { get; set; }

        [BsonElement("studentCountry")]
        public string? StudentCountry { get; set; }

        [BsonElement("studentEd")]
        public string? School { get; set; }

        // Constructor
        public Student(string id, string password, string firstName, string lastName, string email, DateOnly? dob,
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
