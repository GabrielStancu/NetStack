namespace ParticipationService.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Contributer>? Contributers { get; set; }
}
