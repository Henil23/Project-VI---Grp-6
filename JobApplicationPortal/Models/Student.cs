using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace JobApplicationPortal.Models
{
    public class Student(int id, string firstName, string lastName, DateOnly dob,
        string address, string city, string country, string school)
    {
        private int studentID = id;
        private string? firstName = firstName;
        private string? lastName = lastName;
        private DateOnly? studentDOB = dob;
        private string? studentAddress = address;
        private string? studentCity = city;
        private string? studentCountry = country;
        private string? school;

        // Getter and Setter for ID
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        [BsonElement("firstName")]
        // Getter and Setter for FirstName
        public string? FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [BsonElement("lastName")]
        // Getter and Setter for LastName
        public string? LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [BsonElement("DOB")]
        // Getter and Setter for StudentDOB
        public DateOnly? StudentDOB
        {
            get { return studentDOB; }
            set { studentDOB = value; }
        }

        [BsonElement("studentAddress")]
        // Getter and Setter for StudentAddress
        public string? StudentAddress
        {
            get { return studentAddress; }
            set { studentAddress = value; }
        }

        [BsonElement("studentCity")]
        // Getter and Setter for StudentCity
        public string? StudentCity
        {
            get { return studentCity; }
            set { studentCity = value; }
        }

        [BsonElement("studentCountry")]
        // Getter and Setter for StudentCountry
        public string? StudentCountry
        {
            get { return studentCountry; }
            set { studentCountry = value; }
        }

        [BsonElement("school")]
        // Getter and Setter for School
        public string? School
        {
            get { return school; }
            set { school = value; }
        }
    }
}

