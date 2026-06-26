namespace Supply.Infrastructure.EntityConfigurations;

internal class SUIncrementDetailDepartmentEntityTypeConfiguration
    : IEntityTypeConfiguration<SUIncrementDetailDepartment>
{
    public void Configure(EntityTypeBuilder<SUIncrementDetailDepartment> builder)
    {
        builder.ToTable("SupplyDetailDepartment");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("SupplyDetailId")
            .IsRequired();

        builder.Property(f => f.TenantId);

        builder.Property(f => f.OrganizationUnitId);

        builder.Property(f => f.SortOrder)
            .IsRequired();

        builder.Property(f => f.AllocationTime);

        builder.Property(f => f.RemainingAllocationTime);

        builder.Property(f => f.Quantity);

        builder.Property(f => f.UnitPrice);

        builder.Property(f => f.Amount);

        builder.Property(f => f.AllocatedAmount);

        builder.Property(f => f.OrganizationUnitCode)
            .HasMaxLength(50);

        builder.Property(f => f.OrganizationUnitName)
            .HasMaxLength(200);

        builder.Property(f => f.OrganizationUnitType);

        builder.Property(f => f.State)
            .HasDefaultValue(0);

        builder.Property(f => f.EditVersion);

        builder.HasOne<SUIncrement>()
            .WithMany(r => r.SUIncrementDetailDepartments)
            .HasForeignKey("SupplyId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
