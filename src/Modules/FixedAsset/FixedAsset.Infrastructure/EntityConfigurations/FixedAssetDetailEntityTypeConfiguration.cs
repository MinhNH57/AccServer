namespace FixedAsset.Infrastructure.EntityConfigurations;

internal class FixedAssetDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<FixedAssetDetail>
{
    public void Configure(EntityTypeBuilder<FixedAssetDetail> builder)
    {
        builder.ToTable("FixedAssetDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("FixedAssetDetailId")
            .IsRequired();

        builder.Property(f => f.SortOrder);

        builder.Property(f => f.Quantity);

        builder.Property(f => f.Description)
            .HasMaxLength(250);

        builder.Property(f => f.WarrantyTime)
            .HasMaxLength(100);

        builder.Property(f => f.Unit)
            .HasMaxLength(100);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.State)
            .HasDefaultValue(0);

        builder.HasOne<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset>()
            .WithMany(r => r.FixedAssetDetails)
            .HasForeignKey("FixedAssetId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
