using ParticipationService.Data;
using ParticipationService.Models;

namespace ParticipationService.DataSeed;

public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        OneToManyRelationship();
        OneToManyFullyDefinedRelationships();
        ManyToManyRelationship();
        OneToOneRelationship();
        ManyToManyNoJoinRelationship();
    }

    private void OneToManyRelationship()
    {
        var course = new Course
        {
            Name = "Computer Science",
            Students = new List<Student>()
            {
                new Student { Name = "James" },
                new Student { Name = "Mathew" },
                new Student { Name = "John" },
                new Student { Name = "Luke" }
            }
        };
        _context.Add(course);
        _context.SaveChanges();
    }

    private void OneToManyFullyDefinedRelationships()
    {
        var customer = new Customer
        {
            Name = "Robert",
            Orders = new List<Order>()
            {
                new Order { Description = "Order 1" },
                new Order { Description = "Order 2" },
                new Order { Description = "Order 3" },
                new Order { Description = "Order 4" }
            }
        };
        _context.Add(customer);
        _context.SaveChanges();
    }

    private void ManyToManyRelationship()
    {
        var actor1 = new Actor { Name = "Marlon Brando" };
        var actor2 = new Actor { Name = "Al Pacino" };
        _context.Add(actor1);
        _context.Add(actor2);

        var movie1 = new Movie { Name = "The Godfather" };
        var movie2 = new Movie { Name = "Scarface" };
        _context.Add(movie1);
        _context.Add(movie2);
        _context.SaveChanges();

        var movieActor1 = new MovieActor() { ActorId = actor1.Id, MovieId = movie1.Id };
        var movieActor2 = new MovieActor() { ActorId = actor2.Id, MovieId = movie1.Id };
        var movieActor3 = new MovieActor() { ActorId = actor2.Id, MovieId = movie2.Id };

        _context.Add(movieActor1);
        _context.Add(movieActor2);
        _context.Add(movieActor3);
        _context.SaveChanges();
    }

    private void OneToOneRelationship()
    {
        var author = new Author { Name = "Robert Cecil Martin" };
        _context.Add(author);
        _context.SaveChanges();

        var authorBiography = new Biography { AuthorId = author.Id, PlaceOfBirth = "Palo Alto - California" };
        _context.Add(authorBiography);
        _context.SaveChanges();
    }

    private void ManyToManyNoJoinRelationship()
    {
        var project1 = new Project { Name = "NVision" };
        var project2 = new Project { Name = "ByteFin" };

        _context.AddRange(
            new Contributer
            {
                Name = "Gabriel Stancu",
                Projects = new List<Project> { project1, project2 }
            },
            new Contributer
            {
                Name = "Dev Devson",
                Projects = new List<Project> { project2 }
            });

        _context.SaveChanges();
    }
}