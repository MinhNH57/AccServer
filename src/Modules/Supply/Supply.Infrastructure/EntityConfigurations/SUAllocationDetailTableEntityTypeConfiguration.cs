namespace Supply.Infrastructure.EntityConfigurations;

public class SUAllocationDetailTableEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAllocationDetailTable>
{
    public void Configure(EntityTypeBuilder<SUAllocationDetailTable> builder)
    {
        builder.ToTable("SUAllocationDetailTable");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.TenantId);

        builder.Property(x => x.SupplyId);

        builder.Property(x => x.SupplyCode)
            .HasMaxLength(50);

        builder.Property(x => x.SupplyName)
            .HasMaxLength(200);

        builder.Property(x => x.AllocationObjectId);

        builder.Property(x => x.AllocationRate);

        builder.Property(x => x.AllocationAmount);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.Property(x => x.CostAccount)
            .HasMaxLength(50);

        builder.Property(x => x.ExpenseItemId);

        builder.Property(x => x.TotalAllocationAmount);

        builder.Property(x => x.State)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.ExpenseItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.ExpenseItemName)
            .HasMaxLength(250);

        builder.Property(x => x.AllocationObjectCode)
            .HasMaxLength(50);

        builder.Property(x => x.AllocationObjectName)
            .HasMaxLength(250);

        builder.Property(x => x.AllocationAccount)
            .HasMaxLength(50);

        builder.Property(x => x.AllocationObjectType);

        builder.Property(x => x.ListItemId);

        builder.Property(x => x.ListItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.ListItemName)
            .HasMaxLength(250);

        builder.HasOne<SUAllocation>()
            .WithMany(r => r.SUAllocationDetailTables)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
