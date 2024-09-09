using ApplicationService.Model;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }

        public DbSet<TrainingRequest> trainingRequests { get; set; }
    }
}
