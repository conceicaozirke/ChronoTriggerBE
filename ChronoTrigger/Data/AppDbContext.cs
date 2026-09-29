using ChronoTrigger.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ChronoTrigger.Data



{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<HourlyRate> HourlyRates=> Set<HourlyRate>();
        public DbSet<Activity> Activities=> Set<Activity>();
        public DbSet<TimeEntry> TimeEntries=> Set<TimeEntry>();
        public DbSet<ActiveTimer> ActiveTimers=> Set<ActiveTimer>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HourlyRate>()
            .Property(h => h.RatePerHour)
            .HasPrecision(10, 2);


            modelBuilder.Entity<ActiveTimer>()
            .HasIndex(a => a.ActivityID)
            .IsUnique();
        }

        }
}
