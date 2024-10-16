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
    public DbSet<TeamEmployee> TeamEmployees { get; set; }

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
        .HasForeignKey(st => st.ParentTaskId)
        .OnDelete(DeleteBehavior.Cascade);


      modelBuilder.Entity<SubTask>()
          .HasOne(st => st.Employee)
          .WithMany(e => e.SubTasks)
          .HasForeignKey(st => st.EmployeeId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<TeamEmployee>()
        .HasKey(te => new { te.TeamId, te.EmployeeId });

      modelBuilder.Entity<TeamEmployee>()
       .HasIndex(te => te.TeamId);

      modelBuilder.Entity<TeamEmployee>()
          .HasIndex(te => te.EmployeeId);

      modelBuilder.Entity<TeamEmployee>()
          .HasOne(te => te.Team)
          .WithMany(t => t.TeamEmployees)
          .HasForeignKey(te => te.TeamId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<TeamEmployee>()
          .HasOne(te => te.Employee)
          .WithMany(e => e.TeamEmployees)
          .HasForeignKey(te => te.EmployeeId)
          .OnDelete(DeleteBehavior.Cascade);

    }


  }
}
