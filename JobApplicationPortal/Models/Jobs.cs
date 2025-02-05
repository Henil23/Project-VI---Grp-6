using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationPortal.Models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        //Job ID
        public string? JobID { get; set; }

      
        // Job Title
        [BsonElement("JobTitle")]
        [Required(ErrorMessage = "Job Title is required .")]
        public string? JobTitle { get; set; }

        // Job Description
        [BsonElement("JobDescription")]
        [Required(ErrorMessage = "Job Description is required.")]
        public string? JobDescription { get; set; }
        
        //Company Name
        [BsonElement("CompanyName")]
        [Required(ErrorMessage = "company name is required.")]
       
        public string? CompanyName { get; set; }

       
        // Location 
        [BsonElement("Location")]
        [Required(ErrorMessage = "Location is required.")]
        public string? Location { get; set; }

  

        public bool IsSignedIn { get; set; }

        // Parameterless constructor
        public Job() { }

        // Constructor
        public Job(string id,string jobtitle, string jobdescription, string cname, string location)
        {
            JobID = id;
            JobTitle = jobtitle;
            JobDescription = jobdescription;
            CompanyName = cname;
            Location = location;
        
            IsSignedIn = false;
        }
    }
}
