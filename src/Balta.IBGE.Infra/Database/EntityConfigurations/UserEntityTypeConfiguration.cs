using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Balta.IBGE.Domain.Accounts.Entities;

namespace Balta.IBGE.Infra.Database.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.Email)
            .Property(e => e.Address)
            .HasColumnName("Email")
            .HasMaxLength(256)
            .IsRequired();

        builder.OwnsOne(u => u.Password)
            .Property(p => p.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();
    }
}