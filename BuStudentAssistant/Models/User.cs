using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace BuStudentAssistant.Models
{
    public class User
    {
        [Required]
        [BsonElement("username")]
        public string Username { get; set; }

        [Required]
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
