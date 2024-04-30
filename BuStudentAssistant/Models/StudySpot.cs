using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace BuStudentAssistant.Models
{
    public class StudySpot
    {
        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Description { get; set; }

        public string? Image { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}

