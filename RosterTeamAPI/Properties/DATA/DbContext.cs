// Alias the Task model
using Microsoft.EntityFrameworkCore;
using RosterTeamAPI.Models;

namespace RosterTeamAPI.DATA
{
  public class AppDbContext : DbContext
  {
    public DbSet<Service> Services { get; set; } // Use the alias instead of full namespace
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SubTask> Subtasks { get; set; }
    public DbSet<Team> Teams { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<SubTask>()
          .HasOne(s => s.ParentTask)
          .WithMany(s => s.SubTasks)
          .HasForeignKey(s => s.ParentTaskId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Service>()
        .HasMany(s => s.SubTasks)
        .WithOne(st => st.ParentTask)
        .HasForeignKey(st => st.ParentTaskId);

      modelBuilder.Entity<SubTask>()
          .HasOne(st => st.Employee)
          .WithMany(e => e.SubTasks)
          .HasForeignKey(st => st.EmployeeId);

    }


  }
}
