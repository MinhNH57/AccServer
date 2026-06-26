namespace Supply.Infrastructure.EntityConfigurations;

internal class SUIncrementEntityTypeConfiguration : IEntityTypeConfiguration<SUIncrement>
{
    public void Configure(EntityTypeBuilder<SUIncrement> builder)
    {
        builder.ToTable("Supply");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("SupplyId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(f => f.TenantId);

        builder.Property(f => f.BranchId);

        builder.Property(f => f.SupplyCategoryId);

        builder.Property(f => f.SUAuditRefId);

        builder.Property(f => f.SupplyOtherBookId);

        builder.Property(f => f.FADecrementRefId);

        builder.Property(f => f.RefType)
            .IsRequired();

        builder.Property(f => f.AllocationTime);

        builder.Property(f => f.RemainingAllocationTime);

        builder.Property(f => f.DisplayOnBook);

        builder.Property(f => f.RefOrder);

        builder.Property(f => f.RefDate);

        builder.Property(f => f.CreatedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(f => f.ModifiedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(f => f.IsPostedManagement)
            .IsRequired();

        builder.Property(f => f.IsPostedFinance)
            .IsRequired();

        builder.Property(f => f.SuspendAllocate)
            .IsRequired();

        builder.Property(f => f.Quantity);

        builder.Property(f => f.UnitPrice);

        builder.Property(f => f.Amount);

        builder.Property(f => f.AllocatedAmount);

        builder.Property(f => f.RemainingAmount);

        builder.Property(f => f.TermlyAllocationAmount);

        builder.Property(f => f.SupplyCode)
            .HasMaxLength(50);

        builder.Property(f => f.SupplyName)
            .HasMaxLength(200);

        builder.Property(f => f.RefNo)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(f => f.RefNo)
            .IsUnique();

        builder.Property(f => f.Unit)
            .HasMaxLength(50);

        builder.Property(f => f.AllocationAccount)
            .HasMaxLength(50);

        builder.Property(f => f.CreatedBy)
            .HasMaxLength(100);

        builder.Property(f => f.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(f => f.InPuRefDetailId);

        builder.Property(f => f.ReasonIncrement)
            .HasMaxLength(250);

        builder.Property(f => f.SupplyGroup)
            .HasMaxLength(200);

        builder.Property(f => f.SupplyCategoryCode)
            .HasMaxLength(50);

        builder.Property(f => f.SupplyCategoryName)
            .HasMaxLength(200);

        builder.Property(f => f.State);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.BranchName)
            .HasMaxLength(200);

        builder.Property(f => f.ReasonInactive)
            .HasMaxLength(200);

        builder.HasMany(f => f.SUIncrementDetailDepartments)
            .WithOne()
            .HasForeignKey("SupplyId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.SUIncrementDetailAllocations)
            .WithOne()
            .HasForeignKey("SupplyId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.SUIncrementDetails)
            .WithOne()
            .HasForeignKey("SupplyId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.SUIncrementDetailSources)
            .WithOne()
            .HasForeignKey("SupplyId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
