using Microsoft.EntityFrameworkCore;
using ParticipationService.Data;
using ParticipationService.DataSeed;

// 1-N relationships:
//     1) Do not make them fully-defined (see Student-Course relationship)
//        No additional property for id in FKs, no DbSet in the context class
//        But the mapping relation has to be specified explicitly
//     2) Fully-defined (see Order-Customer relationship)
//        Additional property for id, DbSet for each entity in the context class
//
// N-N relationships:
//     See the Movie-MovieActor-Actor relationship
//     Movie and Actor need join table MovieActor that defines 2 1-N relationships
//     Fully-defined, although special configuration has to be defined in the context
//     To tell EF the primary key of the join table MovieActor is composed

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
