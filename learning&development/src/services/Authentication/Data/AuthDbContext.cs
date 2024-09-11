using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AuthAPI.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Register> Registers { get; set; }

        public DbSet<TrainingRequest> TrainingRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the foreign key relationship
            modelBuilder.Entity<TrainingRequest>()
                .HasOne(tr => tr.Registers)          // Navigation property in TrainingRequest
                .WithMany()                         // No collection navigation property in Register
                .HasForeignKey(tr => tr.EmployeeId) // Foreign key in TrainingRequest
                .OnDelete(DeleteBehavior.Restrict); // Configure delete behavior if needed
        }
    }
}
