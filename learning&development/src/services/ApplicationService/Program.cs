using Carter;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AuthAPI.Data; // Ensure this namespace is included for AuthDbContext

var builder = WebApplication.CreateBuilder(args);

// Register AuthDbContext
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("AuthConnection");
    options.UseSqlServer(connection);
});

// Register MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Register Carter
builder.Services.AddCarter();

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection(); // Enforce HTTPS if needed
app.UseRouting();
app.UseAuthorization(); // Ensure this is included to use authorization middleware

// Map Carter modules, which should include your endpoints
app.MapCarter();

// Simple root endpoint for testing
app.MapGet("/", () => "Hello World");

app.Run();
