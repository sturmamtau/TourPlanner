using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;

namespace TourPlanner.DAL;

public class TourPlannerContext : DbContext
{
    public TourPlannerContext(DbContextOptions<TourPlannerContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourLog> TourLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // enum mapping -> save as string in db
        modelBuilder.Entity<Tour>()
            .Property(t => t.TransportType)
            .HasConversion<string>();
    }
}