namespace ParticipationService.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Course? Course { get; set; }
}
