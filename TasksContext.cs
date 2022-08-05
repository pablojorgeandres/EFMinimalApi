using Microsoft.EntityFrameworkCore;
using EFMinimalApi.Models;

namespace EFMinimalApi;

public class TasksContext : DbContext 
{
    public DbSet<Category> Categories { get;set; }
    public DbSet<EFMinimalApi.Models.Task> Tasks { get;set; }
}