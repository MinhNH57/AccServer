using Auth.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.API.Data.Configurations;

public class PendingUserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<PendingUserRegistration>
{
    public void Configure(EntityTypeBuilder<PendingUserRegistration> builder)
    {
        builder.ToTable("PendingUserRegistration");

        builder.Property(e => e.Id).HasDefaultValueSql("NEWID()");
        builder.Property(e => e.Created).HasDefaultValueSql("GETDATE()");
    }
}