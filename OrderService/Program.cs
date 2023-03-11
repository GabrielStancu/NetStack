using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Extensions;
using OrderService.MessageBrokerLibrary;
using OrderService.Repositories;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// it creates connection to RabbitMQ server
// Format "amqp://{username}/{password}@{url}:{RabbitMQ port}
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));

// After building connection, then create exchange (name, type, pattern) and queue name through ISubscriber
builder.Services.AddSingleton<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>()!,
    "order-exchange",
    "order-food-queue",
    "order.*",
    ExchangeType.Topic));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>(); // custom global exception

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();