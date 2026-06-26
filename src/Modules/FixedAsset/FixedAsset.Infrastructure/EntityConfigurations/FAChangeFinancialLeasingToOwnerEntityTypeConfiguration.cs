namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FAChangeFinancialLeasingToOwnerEntityTypeConfiguration
    : IEntityTypeConfiguration<FAChangeFinancialLeasingToOwner>
{
    public void Configure(EntityTypeBuilder<FAChangeFinancialLeasingToOwner> builder)
    {
        builder.ToTable("FAChangeFinancialLeasingToOwner");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.TenantId);

        builder.Property(x => x.BranchId);

        builder.Property(x => x.FixedAssetId);

        builder.Property(x => x.FixedAssetName)
            .HasMaxLength(200);

        builder.Property(x => x.DisplayOnBook);

        builder.Property(x => x.RefType)
            .IsRequired();

        builder.Property(x => x.RefOrder);

        builder.Property(x => x.JournalMemo)
            .HasMaxLength(512);

        builder.Property(x => x.PostedDate);

        builder.Property(x => x.RefDate);

        builder.Property(x => x.RefNo)
            .HasMaxLength(50);

        builder.Property(x => x.FixedAssetCode)
            .HasMaxLength(200);

        builder.Property(x => x.TotalAmount);

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

        builder.Property(x => x.OldOrgPriceAccount)
         .HasMaxLength(100);

        builder.Property(x => x.NewOrgPriceAccount)
         .HasMaxLength(100);

        builder.Property(x => x.OldDepreciationAccount)
         .HasMaxLength(100);

        builder.Property(x => x.NewDepreciationAccount)
         .HasMaxLength(100);

        builder.Property(x => x.OldOrgPrice)
            .IsRequired();

        builder.Property(x => x.NewOrgPrice)
            .IsRequired();

        builder.Property(x => x.OldDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.NewDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.OldAccumDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.NewAccumDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.OldRemainingAmount)
            .IsRequired();

        builder.Property(x => x.NewRemainingAmount)
            .IsRequired();

        builder.Property(x => x.OldLifeTime)
            .IsRequired();

        builder.Property(x => x.NewLifeTime)
            .IsRequired();

        builder.Property(x => x.OldLifeTimeRemaining)
            .IsRequired();

        builder.Property(x => x.NewLifeTimeRemaining)
            .IsRequired();

        builder.Property(x => x.OldDepreciationRateMonth)
            .IsRequired();

        builder.Property(x => x.NewDepreciationRateMonth)
            .IsRequired();

        builder.Property(x => x.OldMonthlyDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.NewMonthlyDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.OldDepreciationRateYear)
            .IsRequired();

        builder.Property(x => x.NewDepreciationRateYear)
            .IsRequired();

        builder.Property(x => x.OldYearlyDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.NewYearlyDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.OldIsLimitDepreciationAmount)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.NewIsLimitDepreciationAmount)
             .IsRequired()
             .HasDefaultValue(false);

        builder.Property(x => x.OldDepreciationAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.NewDepreciationAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.OldRemainingAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.NewRemainingAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.OldMonthlyDepreciationAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.NewMonthlyDepreciationAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
         .HasMaxLength(100);

        builder.Property(x => x.ModifiedBy)
         .HasMaxLength(100);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.DifferentOrgPrice)
            .IsRequired();

        builder.Property(x => x.DifferentDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.DifferentAccumDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.DifferentRemainingAmount)
            .IsRequired();

        builder.Property(x => x.DifferentLifeTime)
            .IsRequired();

        builder.Property(x => x.DifferentLifeTimeRemaining)
            .IsRequired();

        builder.Property(x => x.DifferentDepreciationRateMonth)
            .IsRequired();

        builder.Property(x => x.DifferentMonthlyDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.DifferentDepreciationRateYear)
            .IsRequired();

        builder.Property(x => x.DifferentYearlyDepreciationAmount)
            .IsRequired();

        builder.Property(x => x.DifferentDepreciationAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.DifferentRemainingAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.DifferentMonthlyDepreciationAmountByIncomeTax)
            .IsRequired();

        builder.Property(x => x.AttachmentIdList);

        builder.Property(x => x.BranchName)
         .HasMaxLength(200);

        builder.HasMany(x => x.FAChangeFinancialLeasingToOwnerDetails)
         .WithOne()
         .HasForeignKey("RefId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
