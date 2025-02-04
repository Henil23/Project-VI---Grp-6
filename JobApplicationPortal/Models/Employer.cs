using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationPortal.Models
{
    public class Employer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? EmployerID { get; set; }

        [BsonElement("CompanyName")]
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Company Name must be between 1 and 100 characters.")]
        public string? CompanyName { get; set; }

        [BsonElement("email")]
        [Required]
        [EmailAddress]
        public string? EmployerEmail {  get; set; }

        [BsonElement("password")]
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")] // password validation
        public string? EmployerPassword { get; set; }
    }
}
