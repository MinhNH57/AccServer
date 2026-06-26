namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FADepreciationDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<FADepreciationDetail>
{
    public void Configure(EntityTypeBuilder<FADepreciationDetail> builder)
    {
        builder.ToTable("FADepreciationDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.FixedAssetId);

        builder.Property(x => x.OrganizationUnitId);

        builder.Property(x => x.SortOrder)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.MonthlyDepreciationAmount);

        builder.Property(x => x.AmountReasonableCost);

        builder.Property(x => x.AmountUnReasonableCost);

        builder.Property(x => x.OrganizationUnitCode)
            .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
            .HasMaxLength(250);

        builder.Property(x => x.FixedAssetCode)
            .HasMaxLength(50);

        builder.Property(x => x.FixedAssetName)
         .HasMaxLength(200);

        builder.Property(x => x.FixedAssetCategoryName)
         .HasMaxLength(200);

        builder.Property(x => x.FixedAssetCategoryId)
         .IsRequired();

        builder.Property(x => x.FixedAssetCategoryCode)
          .HasMaxLength(50);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.HasOne<FADepreciation>()
            .WithMany(r => r.FADepreciationDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
