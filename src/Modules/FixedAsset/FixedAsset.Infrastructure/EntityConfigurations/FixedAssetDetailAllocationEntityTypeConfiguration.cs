namespace FixedAsset.Infrastructure.EntityConfigurations;

internal class FixedAssetDetailAllocationEntityTypeConfiguration
    : IEntityTypeConfiguration<FixedAssetDetailAllocation>
{
    public void Configure(EntityTypeBuilder<FixedAssetDetailAllocation> builder)
    {
        builder.ToTable("FixedAssetDetailAllocation");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("FixedAssetDetailId")
            .IsRequired();

        builder.Property(f => f.ObjectId)
            .IsRequired();

        builder.Property(f => f.ExpenseItemId);

        builder.Property(f => f.ListItemId);

        builder.Property(f => f.SortOrder);

        builder.Property(f => f.ObjectType);

        builder.Property(f => f.AllocationRate);

        builder.Property(f => f.CostAccount)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(f => f.ObjectCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(f => f.ObjectName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(f => f.ExpenseItemCode)
            .HasMaxLength(100);

        builder.Property(f => f.ListItemCode)
            .HasMaxLength(100);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.State)
            .HasDefaultValue(0);

        builder.HasOne<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset>()
            .WithMany(r => r.FixedAssetDetailAllocations)
            .HasForeignKey("FixedAssetId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
