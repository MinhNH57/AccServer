namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FADecrementDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<FADecrementDetail>
{
    public void Configure(EntityTypeBuilder<FADecrementDetail> builder)
    {
        builder.ToTable("FADecrementDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.FixedAssetId);

        builder.Property(x => x.FixedAssetCode)
         .IsRequired()
         .HasMaxLength(50);

        builder.Property(x => x.FixedAssetName)
         .IsRequired()
         .HasMaxLength(200);

        builder.Property(x => x.OrganizationUnitId);

        builder.Property(x => x.SortOrder)
         .IsRequired();

        builder.Property(x => x.OrgPrice)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.DepreciationAmount)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.AccumDepreciationAmount)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.RemainingAmount)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.DepreciationAmountInMonth)
         .IsRequired()
         .HasDefaultValue(0.0d);

        builder.Property(x => x.OrgPriceAccount)
         .IsRequired()
         .HasMaxLength(50);

        builder.Property(x => x.DepreciationAccount)
         .IsRequired()
         .HasMaxLength(50);

        builder.Property(x => x.RemainingAccount)
         .IsRequired()
         .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitCode)
         .IsRequired()
         .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
         .IsRequired()
         .HasMaxLength(200);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.HasOne<FADecrement>()
            .WithMany(r => r.FADecrementDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
