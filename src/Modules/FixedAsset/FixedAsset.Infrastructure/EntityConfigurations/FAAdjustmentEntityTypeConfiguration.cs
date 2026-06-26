namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FAAdjustmentEntityTypeConfiguration
    : IEntityTypeConfiguration<FAAdjustment>
{
    public void Configure(EntityTypeBuilder<FAAdjustment> builder)
    {
        builder.ToTable("FAAdjustment");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.TenantId);

        builder.Property(x => x.BranchId);

        builder.Property(x => x.BranchName)
         .HasMaxLength(200);

        builder.Property(x => x.RefType)
         .IsRequired();

        builder.Property(x => x.DisplayOnBook)
         .IsRequired();

        builder.Property(x => x.RefOrder)
         .IsRequired();

        builder.Property(x => x.RefDate);

        builder.Property(x => x.PostedDate);

        builder.Property(x => x.DecisionDate);

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

        builder.Property(x => x.DecisionNo)
         .HasMaxLength(50);

        builder.Property(x => x.Reason)
         .HasMaxLength(512);

        builder.Property(x => x.JournalMemo)
         .HasMaxLength(512);

        builder.Property(x => x.CreatedBy)
         .HasMaxLength(100);

        builder.Property(x => x.ModifiedBy)
         .HasMaxLength(100);

        builder.Property(x => x.State)
         .IsRequired();

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.Members)
         .HasColumnType("nvarchar(max)");

        builder.Property(x => x.TotalAmount)
         .IsRequired();

        builder.Property(x => x.AttachmentIdList)
         .HasColumnType("nvarchar(max)");

        builder.HasMany(x => x.FAAdjustmentDetails)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.FAAdjustmentDetailPosts)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
