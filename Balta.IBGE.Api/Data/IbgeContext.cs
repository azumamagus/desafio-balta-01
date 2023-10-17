using Balta.IBGE.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Balta.IBGE.Api.Data;

public class IbgeContext : DbContext
{
    public IbgeContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost,1433;Database=ibge;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasKey(c => c.Id);
    }
}
