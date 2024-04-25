using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace BuStudentAssistant.Models
{
    public class BusyScore
    {
        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Score{ get; set; }
        
        [Required]
        public int User_id { get; set; }

        [Required]
        public MongoDB.Bson.ObjectId Spot_id { get; set;}
    }
}

