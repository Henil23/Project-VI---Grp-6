using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Job
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; }

    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("Company")]
    public string Company { get; set; }

    [BsonElement("PostedDate")]
    public DateTime PostedDate { get; set; }
}
