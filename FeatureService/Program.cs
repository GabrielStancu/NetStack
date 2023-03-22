using FeatureService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the factory and the services for conditional choice between implementations
builder.Services.AddScoped<ServiceFactory>();
builder.Services.AddTransient<Service1>()
    .AddScoped<IService, Service1>(s => s.GetService<Service1>()!);
builder.Services.AddTransient<Service2>()
    .AddScoped<IService, Service2>(s => s.GetService<Service2>()!);

// Register service with multiple interfaces such that just one instance is created
builder.Services.AddSingleton<Storage>();
builder.Services.AddSingleton<IReadStorage>(sp => sp.GetRequiredService<Storage>());
builder.Services.AddSingleton<IWriteStorage>(sp => sp.GetRequiredService<Storage>());

// Register service with startup logic
builder.Services.AddScoped<IStartupLogic, StartupLogic>();

// Register complex service that needs to create multiple scopes inside
builder.Services.AddScoped<IComplexService, ComplexService>();

var app = builder.Build();

// Get an instance of the service containing startup logic that cannot be instantiated otherwise
using var scope = app.Services.CreateScope();
var startupLogic = scope.ServiceProvider.GetRequiredService<IStartupLogic>();
startupLogic.WarmUp();

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