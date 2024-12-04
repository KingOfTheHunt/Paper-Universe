using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Infra.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> User { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}