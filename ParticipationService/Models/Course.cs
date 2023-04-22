namespace ParticipationService.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Student>? Students { get; set; }
}
