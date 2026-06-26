using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefNo.API.Model;

namespace RefNo.API.Infrastructure.EntityConfigurations;

internal class NumberingRuleEntityTypeConfiguration
    : IEntityTypeConfiguration<NumberingRule>
{
    public void Configure(EntityTypeBuilder<NumberingRule> builder)
    {
        builder.ToTable("CatalogVoucherNumber");

        builder.HasKey(f => f.AutoId);

        builder.Property(f => f.AutoId)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(f => f.TenantId);

        builder.Property(f => f.BranchId);

        builder.Property(f => f.RefTypeCategory);

        builder.Property(f => f.LengthOfValue)
            .HasColumnName("VoucherLength");

        builder.Property(f => f.DisplayOnBook);

        builder.Property(f => f.CreatedDate)
            .HasColumnName("Created")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(f => f.ModifiedDate)
            .HasColumnName("Modified")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(f => f.Value);

        builder.Property(f => f.RefTypeCategoryName)
            .HasColumnName("VoucherName")
            .HasMaxLength(200);

        builder.Property(f => f.RefTypeCategoryNameEnglish)
            .HasMaxLength(200);

        builder.Property(f => f.Prefix)
            .HasColumnName("SignVoucher")
            .HasMaxLength(50);

        builder.Property(f => f.Suffix)
            .HasMaxLength(50);

        builder.Property(f => f.CreatedBy)
            .HasMaxLength(100);

        builder.Property(f => f.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.State)
            .HasDefaultValue(0);

        builder.Property(f => f.DatabaseId);

        builder.Property(f => f.MaxValue);

        builder.Property(f => f.StateMode);
    }
}
