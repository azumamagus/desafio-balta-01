using Balta.IBGE.Domain.Cities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balta.IBGE.Infra.Database.EntityConfigurations;

public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .ToTable("Cities");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .ValueGeneratedNever();

        builder
            .Property(c => c.State)
            .HasMaxLength(2)
            .IsRequired();

        builder
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasIndex(c => new { c.State, c.Name })
            .IsUnique();
    }
}