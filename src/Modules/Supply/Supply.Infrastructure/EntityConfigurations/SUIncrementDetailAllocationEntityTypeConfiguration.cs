namespace Supply.Infrastructure.EntityConfigurations;

internal class SUIncrementDetailAllocationEntityTypeConfiguration
    : IEntityTypeConfiguration<SUIncrementDetailAllocation>
{
    public void Configure(EntityTypeBuilder<SUIncrementDetailAllocation> builder)
    {
        builder.ToTable("SupplyDetailAllocation");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("SupplyDetailId")
            .IsRequired();

        builder.Property(f => f.TenantId);

        builder.Property(f => f.ObjectId);

        builder.Property(f => f.ExpenseItemId);

        builder.Property(f => f.SortOrder)
            .IsRequired();

        builder.Property(f => f.ObjectType);

        builder.Property(f => f.AllocationRate);

        builder.Property(f => f.CostAccount)
            .HasMaxLength(100);

        builder.Property(f => f.ObjectCode)
            .HasMaxLength(50);

        builder.Property(f => f.ObjectName)
            .HasMaxLength(200);

        builder.Property(f => f.State)
            .HasDefaultValue(0);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.ExpenseItemCode)
            .HasMaxLength(50);

        builder.Property(f => f.ListItemId);

        builder.Property(f => f.ListItemCode)
            .HasMaxLength(50);

        builder.HasOne<SUIncrement>()
            .WithMany(r => r.SUIncrementDetailAllocations)
            .HasForeignKey("SupplyId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
