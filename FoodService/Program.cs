using FoodService.Data;
using FoodService.MessageBrokerLibrary;
using FoodService.Repositories;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create connection
// amqp://guest:guest@localhost:5672 => amqp://{username}:{passwoed}@{url}:{rabbitmq port}
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));

// Register IPublisher in DI container
// Define exchange name as well as exchange topic
builder.Services.AddSingleton<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>()!,
    "order-exchange",
    ExchangeType.Topic));

// Settings configuration
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(provider =>
    provider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

// Repositories
builder.Services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();

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
