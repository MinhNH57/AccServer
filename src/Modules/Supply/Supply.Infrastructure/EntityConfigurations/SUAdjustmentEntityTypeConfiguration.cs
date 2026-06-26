namespace Supply.Infrastructure.EntityConfigurations;

public class SUAdjustmentEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAdjustment>
{
    public void Configure(EntityTypeBuilder<SUAdjustment> builder)
    {
        builder.ToTable("SUAdjustment");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.TenantId);

        builder.Property(x => x.BranchId);

        builder.Property(x => x.RefType)
         .IsRequired();

        builder.Property(x => x.DisplayOnBook);

        builder.Property(x => x.RefOrder);

        builder.Property(x => x.JournalMemo)
         .HasMaxLength(255);

        builder.Property(x => x.RefDate);

        builder.Property(x => x.CreatedDate)
         .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.ModifiedDate)
         .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.IsPostedFinance)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.IsPostedManagement)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.RefNo)
         .HasMaxLength(50);

        builder.HasIndex(x => x.RefNo)
         .IsUnique();

        builder.Property(x => x.CreatedBy)
         .HasMaxLength(100);

        builder.Property(x => x.ModifiedBy)
         .HasMaxLength(100);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.TotalAmount);

        builder.Property(x => x.BranchName)
         .HasMaxLength(200);

        builder.Property(x => x.AttachmentIdList)
         .HasColumnType("nvarchar(max)");

        builder.HasMany(x => x.SUAdjustmentDetails)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.SUAdjustmentDetailVouchers)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
