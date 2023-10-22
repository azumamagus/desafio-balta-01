using System.Reflection;

using Balta.IBGE.Domain.Accounts.Entities;
using Balta.IBGE.Domain.Cities;

using Microsoft.EntityFrameworkCore;

namespace Balta.IBGE.Infra.Database;

public class IBGEDbContext : DbContext
{
    public IBGEDbContext(DbContextOptions options)
        : base(options)
    { }

    public DbSet<User> Users{ get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}