//using ApplicationService.Model;
//using Microsoft.EntityFrameworkCore;
//using AuthAPI.Models;

//namespace ApplicationService.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

//        public DbSet<TrainingRequest> TrainingRequests { get; set; }

//        //protected override void OnModelCreating(ModelBuilder modelBuilder)
//        //{
//        //    base.OnModelCreating(modelBuilder);

//        //    // Configure the foreign key relationship
//        //    modelBuilder.Entity<TrainingRequest>()
//        //        .HasOne(tr => tr.Registers)          // Navigation property in TrainingRequest
//        //        .WithMany()                         // No collection navigation property in Register
//        //        .HasForeignKey(tr => tr.EmployeeId) // Foreign key in TrainingRequest
//        //        .OnDelete(DeleteBehavior.Restrict); // Configure delete behavior if needed
//        //}
//    }
//}
