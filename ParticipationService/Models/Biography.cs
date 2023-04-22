namespace ParticipationService.Models;

public class Biography
{
    public int Id { get; set; }
    public string PlaceOfBirth { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}
