namespace Supply.Infrastructure.EntityConfigurations;

public class SUAllocationDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAllocationDetailExpense>
{
    public void Configure(EntityTypeBuilder<SUAllocationDetailExpense> builder)
    {
        builder.ToTable("SUAllocationDetailExpense");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();
        builder.Property(x => x.SupplyCode)
            .HasMaxLength(50);

        builder.Property(x => x.SupplyName)
            .HasMaxLength(200);

        builder.Property(x => x.SupplyCategoryCode)
            .HasMaxLength(50);

        builder.Property(x => x.SupplyCategoryName)
            .HasMaxLength(200);

        builder.Property(x => x.TenantId);

        builder.Property(x => x.SupplyId);

        builder.Property(x => x.SupplyCategoryId);

        builder.Property(x => x.SortOrder)
         .IsRequired();

        builder.Property(x => x.TotalAllocationAmount);

        builder.Property(x => x.AllocationAmount);

        builder.Property(x => x.RemainingAmount);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.HasOne<SUAllocation>()
            .WithMany(r => r.SUAllocationDetailExpenses)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
