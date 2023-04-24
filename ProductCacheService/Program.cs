using Microsoft.EntityFrameworkCore;
using ProductCacheService.Data;
using ProductCacheService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// from Scrutor package:
builder.Services.Decorate<IProductRepository, CacheProductRepository>();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=data.db"));

var app = builder.Build();

// Populate data before the application starts
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
if (await context.Database.EnsureCreatedAsync())
    await context.GenerateProduct(1000);

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
