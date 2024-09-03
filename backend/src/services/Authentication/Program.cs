using AuthAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    var connstring = builder.Configuration.GetConnectionString("AuthConnectionstr");
    options.UseSqlServer(connstring);
});
// Configure the HTTP request pipeline.

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();

app.Run();
