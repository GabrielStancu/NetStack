using Microsoft.EntityFrameworkCore;
using ParticipationService.Models;

namespace ParticipationService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base (options)
    {
    }

    public DbSet<Customer>? Customer { get; set; }
    public DbSet<Order>? Order { get; set; }

    public DbSet<Contributer>? Contributer { get; set; }
    public DbSet<Project>? Project { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OneToManyRelationshipConfiguration(modelBuilder);
        ManyToManyRelationshipConfiguration(modelBuilder);
        OneToOneRelationshipConfiguration(modelBuilder);
    }

    private static void OneToManyRelationshipConfiguration(ModelBuilder modelBuilder)
    {
        // The relationship can be made starting with Course
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne(s => s.Course)
            .IsRequired();

        // But it can start with Student as well:
        // modelBuilder.Entity<Student>()
        //     .HasOne(s => s.Course)
        //     .WithMany(c => c.Students)
        //     .IsRequired();
    }

    private static void ManyToManyRelationshipConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieActor>()
            .HasKey(t => new { t.ActorId, t.MovieId });

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Actor)
            .WithMany(a => a.MovieActors)
            .HasForeignKey(ma => ma.ActorId);

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);
    }

    private static void OneToOneRelationshipConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasOne(a => a.Biography)
            .WithOne(b => b.Author)
            .HasForeignKey<Biography>(b => b.AuthorId);
    }
}
