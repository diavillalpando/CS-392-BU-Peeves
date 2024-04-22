using BuStudentAssistant.Data;
using BuStudentAssistant.Models;
using MongoDB.Driver;

namespace StudySpotYelpNameSpace;

internal class StudySpot
{
    public int Id { get; set; }
    public float Score { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int Map_Marker { get; set; }
    public string? Description { get; set;}
    public List<Comment>? Comments { get; set; }
    
}

// internal class Comment{
//     public int Id { get; set; }
//     public DateOnly Date { get; set; }
//     public string? Text { get; set; }
//     public int Stars { get; set;}
//     public int User_Id { get; set; }
//     public string? User_Name { get; set; }
// }
