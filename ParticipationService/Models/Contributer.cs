namespace ParticipationService.Models;

public class Contributer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Project>? Projects { get; set; }
}
