namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FADepreciationDetailAllocationEntityTypeConfiguration
    : IEntityTypeConfiguration<FADepreciationDetailAllocation>
{
    public void Configure(EntityTypeBuilder<FADepreciationDetailAllocation> builder)
    {
        builder.ToTable("FADepreciationDetailAllocation");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.TenantId);

        builder.Property(x => x.FixedAssetId);

        builder.Property(x => x.OrganizationUnitId);

        builder.Property(x => x.AllocationObjectId);

        builder.Property(x => x.ExpenseItemId);

        builder.Property(x => x.ListItemId);

        builder.Property(x => x.SortOrder)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.MonthlyDepreciationAmount);

        builder.Property(x => x.AllocationRate);

        builder.Property(x => x.AllocationAmount);

        builder.Property(x => x.CostAccount)
            .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
            .HasMaxLength(250);

        builder.Property(x => x.AllocationObjectCode)
            .HasMaxLength(50);

        builder.Property(x => x.AllocationObjectName)
            .HasMaxLength(250);

        builder.Property(x => x.AllocationObjectType);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.FixedAssetCode)
            .HasMaxLength(50);

        builder.Property(x => x.FixedAssetName)
         .HasMaxLength(200);

        builder.Property(x => x.ListItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.ListItemName)
            .HasMaxLength(250);

        builder.Property(x => x.ExpenseItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.ExpenseItemName)
            .HasMaxLength(250);

        builder.Property(x => x.DepreciationAccount)
            .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitCode)
            .HasMaxLength(50);

        builder.HasOne<FADepreciation>()
            .WithMany(r => r.FADepreciationDetailAllocations)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
