using Microsoft.EntityFrameworkCore;
using EFMinimalApi.Models;

#nullable disable

namespace EFMinimalApi;

public class TasksContext : DbContext 
{
    public DbSet<Category> Categories { get;set; }
    public DbSet<EFMinimalApi.Models.Task> Tasks { get;set; }

    public TasksContext(DbContextOptions<TasksContext> options) :base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Category>( x => 
        {
            x.ToTable("Category");

                x.HasKey(p => p.CategoryId);

                x.Property(p => p.Name).IsRequired().HasMaxLength(150);
                x.Property(p => p.Description);
        });
        
        modelBuilder.Entity<EFMinimalApi.Models.Task>( x => 
        {
            x.ToTable("Task");

                x.HasKey(p => p.TaskId);
                x.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
        
                x.Property(p => p.Title).IsRequired().HasMaxLength(200);
                x.Property(p => p.Description);
                x.Property(p => p.TaskPriority);
                x.Property(p => p.CreationDateTime);
                x.Ignore(p => p.Resumen);
        });

    }
}