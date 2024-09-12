using Carter;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AuthAPI.Data; // Ensure this namespace is included for AuthDbContext

var builder = WebApplication.CreateBuilder(args);

// Register AuthDbContext with SQL Server
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("AuthConnection");
    options.UseSqlServer(connection);
});

// Register MediatR with assembly scanning
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Register Carter for routing modules
builder.Services.AddCarter();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:4200")
              .AllowCredentials();
    });
});

// Add SignalR services
builder.Services.AddSignalR();

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(); // Use CORS before Authorization

app.UseAuthorization();

// Map SignalR hubs
app.MapHub<NotificationHub>("/notificationHub");

// Map Carter modules
app.MapCarter();

// Simple root endpoint for testing
app.MapGet("/", () => "Hello World");

app.Run();
