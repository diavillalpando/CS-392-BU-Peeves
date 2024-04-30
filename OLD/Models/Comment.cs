using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace BuStudentAssistant.Models
{
    public class Comment
    {
        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }

        [Required]
        // [BsonElement("username")]
        public DateTime Date { get; set; }

        [Required]
        // [BsonElement("username")]
        public string? Text { get; set; }

        [Required]
        // [BsonElement("username")]
        public int Stars { get; set;}

        [Required]
        // [BsonElement("username")]
        public MongoDB.Bson.ObjectId Spot_id { get; set;}

        [Required]
        // [BsonElement("username")]
        public int User_id { get; set; }

        // [BsonElement("username")]
        public string? User_Name { get; set; }
    }
}

