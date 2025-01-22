using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JobApplicationPortal.Models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("Title")]
        public string? Title { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("Company")]
        public string? Company { get; set; }

        [BsonElement("Location")]
        public string? Location { get; set; }

        [BsonElement("PostedDate")]
        public DateTime PostedDate { get; set; }
    }
}
