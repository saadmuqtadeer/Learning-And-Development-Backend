using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AuthAPI.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Register> registers {  get; set; }
    }

}
