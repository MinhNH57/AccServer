namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FADepreciationEntityTypeConfiguration
    : IEntityTypeConfiguration<FADepreciation>
{
    public void Configure(EntityTypeBuilder<FADepreciation> builder)
    {
        builder.ToTable("FADepreciation");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.TenantId);

        builder.Property(x => x.BranchId);

        builder.Property(x => x.RefType)
         .IsRequired();

        builder.Property(x => x.Month)
            .IsRequired();

        builder.Property(x => x.Year);

        builder.Property(x => x.DisplayOnBook)
            .IsRequired();

        builder.Property(x => x.RefOrder)
         .IsRequired();

        builder.Property(x => x.RefDate);

        builder.Property(x => x.PostedDate);

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

        builder.Property(x => x.TotalAmount)
         .HasDefaultValue(0.0);

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

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.AttachmentIdList);

        builder.Property(x => x.BranchName)
            .HasMaxLength(200);

        builder.HasMany(x => x.FADepreciationDetails)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.FADepreciationDetailAllocations)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.FADepreciationDetailPosts)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
