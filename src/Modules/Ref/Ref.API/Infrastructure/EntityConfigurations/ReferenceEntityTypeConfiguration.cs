using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ref.API.Model;

namespace Ref.API.Infrastructure.EntityConfigurations;

public class ReferenceEntityTypeConfiguration : IEntityTypeConfiguration<Reference>
{
    public void Configure(EntityTypeBuilder<Reference> builder)
    {
        builder.ToTable("Reference");

        builder.HasKey(x => x.ReferenceId);

        builder.Property(x => x.ReferenceId)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.RefId1)
            .IsRequired();

        builder.Property(x => x.RefId2)
            .IsRequired();

        builder.Property(x => x.RefType1)
            .IsRequired();

        builder.Property(x => x.RefType2)
            .IsRequired();

        builder.Property(x => x.ReferenceType)
            .IsRequired();

        builder.Property(x => x.SortOrder)
            .HasDefaultValue(0);

        builder.Property(x => x.RefNoFinance2)
            .HasMaxLength(50);

        builder.Property(x => x.RefNoManagement2)
            .HasMaxLength(50);

        builder.Property(x => x.TotalAmountOc);

        builder.Property(x => x.Amount);

        builder.Property(x => x.State)
            .HasDefaultValue(0);

        builder.HasIndex(x => new { x.RefId1, x.RefId2, x.ReferenceType });
    }
}
