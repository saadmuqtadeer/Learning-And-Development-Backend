using ApplicationService.Data;
using AuthAPI.Data;
using Carter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c => {
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("ApplicationConnection");
    options.UseSqlServer(connection);
});

var app = builder.Build();

app.MapCarter();

app.MapGet("/", () => "Hello World");

app.Run();
