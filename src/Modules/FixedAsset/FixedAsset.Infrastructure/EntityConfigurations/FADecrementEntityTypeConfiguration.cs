namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FADecrementEntityTypeConfiguration
    : IEntityTypeConfiguration<FADecrement>
{
    public void Configure(EntityTypeBuilder<FADecrement> builder)
    {
        builder.ToTable("FADecrement");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.BranchId);

        builder.Property(x => x.RefType)
         .IsRequired();

        builder.Property(x => x.DisplayOnBook)
         .HasDefaultValue(0);

        builder.Property(x => x.RefOrder)
         .IsRequired();

        builder.Property(x => x.RefDate)
         .IsRequired();

        builder.Property(x => x.PostedDate)
         .IsRequired();

        builder.Property(x => x.IsPostedFinance)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.IsPostedManagement)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.TotalAmount)
         .IsRequired()
         .HasDefaultValue(0.0);

        builder.Property(x => x.RefNo)
         .IsRequired()
         .HasMaxLength(50);

        builder.HasIndex(x => x.RefNo)
         .IsUnique();

        builder.Property(x => x.JournalMemo)
         .HasMaxLength(512);

        builder.Property(x => x.BranchName)
         .HasMaxLength(200);

        builder.Property(x => x.AttachmentIdList);

        builder.Property(x => x.CreatedDate)
         .IsRequired()
         .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.CreatedBy)
         .HasMaxLength(100);

        builder.Property(x => x.ModifiedDate)
         .IsRequired()
         .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.ModifiedBy)
         .HasMaxLength(100);

        builder.Property(x => x.AutoRefNo)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.ForceUpdate)
         .IsRequired()
         .HasDefaultValue(true);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(1);

        builder.HasMany(x => x.FADecrementDetails)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.FADecrementDetailPosts)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
