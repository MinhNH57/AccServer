namespace Supply.Infrastructure.EntityConfigurations;

public class SUAllocationEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAllocation>
{
    public void Configure(EntityTypeBuilder<SUAllocation> builder)
    {
        builder.ToTable("SUAllocation");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.TenantId);

        builder.Property(x => x.BranchId);

        builder.Property(x => x.RefType)
            .IsRequired();

        builder.Property(x => x.Month);

        builder.Property(x => x.Year);

        builder.Property(x => x.DisplayOnBook);

        builder.Property(x => x.RefOrder);

        builder.Property(x => x.RefDate);

        builder.Property(x => x.PostedDate);

        builder.Property(x => x.CreatedDate)
         .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.ModifiedDate)
         .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.IsPostedManagement)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.IsPostedFinance)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.IsGetSupplyAllocated)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.TotalAmount);

        builder.Property(x => x.RefNo)
         .HasMaxLength(50);

        builder.Property(x => x.JournalMemo)
         .HasMaxLength(512);

        builder.Property(x => x.CreatedBy)
         .HasMaxLength(100);

        builder.Property(x => x.ModifiedBy)
         .HasMaxLength(100);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.BranchName)
            .HasMaxLength(200);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.AttachmentIdList);

        builder.HasMany(x => x.SUAllocationDetailExpenses)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.SUAllocationDetailTables)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.SUAllocationDetailPosts)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
