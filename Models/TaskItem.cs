using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace WorkTracker.Models;

public class TaskItem 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string? TaskName {get; set;}
    public string? Description {get;set;}
    public bool isComplete {get;set;}
    public string? Secret {get;set;}
}



// public class TaskItemDTO
// {
//     public long Id { get; set;}
//     public string? Name {get; set;}
//     public string? Description {get;set;}
//     public bool isComplete {get;set;}
// }