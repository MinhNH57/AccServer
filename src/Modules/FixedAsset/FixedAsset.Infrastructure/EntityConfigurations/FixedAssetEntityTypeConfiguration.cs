namespace FixedAsset.Infrastructure.EntityConfigurations;

internal class FixedAssetEntityTypeConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset>
{
    public void Configure(EntityTypeBuilder<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset> builder)
    {
        builder.ToTable("FixedAsset");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("FixedAssetId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(f => f.FixedAssetCategoryId)
            .IsRequired();

        builder.Property(f => f.OrganizationUnitId)
            .IsRequired();

        builder.Property(f => f.BranchId);

        builder.Property(f => f.AccountObjectId);

        builder.Property(f => f.AccountObjectCode)
            .HasMaxLength(50);

        builder.Property(f => f.RefId)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(f => f.ProductionYear);

        builder.Property(f => f.Quality)
            .HasDefaultValue(1);

        builder.Property(f => f.LifeTimeUnit)
            .HasDefaultValue(-1);

        builder.Property(f => f.LifeTimeRemainingUnit)
            .HasDefaultValue(1);

        builder.Property(f => f.RefOrder)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();

        builder.Property(f => f.Inactive)
            .HasDefaultValue(0);

        builder.Property(f => f.DisplayOnBook)
            .HasDefaultValue(0);

        builder.Property(f => f.RefDate)
            .IsRequired();

        builder.Property(f => f.DeliveryRecordDate);

        builder.Property(f => f.DepreciationDate)
            .IsRequired();

        builder.Property(f => f.IsNotDepreciation)
            .HasDefaultValue(false);

        builder.Property(f => f.IsLimitDepreciationAmount)
            .HasDefaultValue(false);

        builder.Property(f => f.IsEnoughVoucher)
            .HasDefaultValue(false);

        builder.Property(f => f.IsPostedManagement)
            .HasDefaultValue(false);

        builder.Property(f => f.IsPostedFinance)
            .HasDefaultValue(true);

        builder.Property(f => f.IsFixedAssetOfStateBudget);

        builder.Property(f => f.Quantity)
            .HasDefaultValue(0);

        builder.Property(f => f.OrgPrice);

        builder.Property(f => f.DepreciationAccount);

        builder.Property(f => f.LifeTime);

        builder.Property(f => f.LifeTimeRemaining);

        builder.Property(f => f.DepreciationRateMonth);

        builder.Property(f => f.RemainingAmountByIncomeTax)
            .HasDefaultValue(0.0d);

        builder.Property(f => f.MonthlyDepreciationAmountByIncomeTax);

        builder.Property(f => f.MonthlyDepreciationAmount);

        builder.Property(f => f.YearlyDepreciationAmount);

        builder.Property(f => f.AccumDepreciationAmount);

        builder.Property(f => f.RemainingAmount);

        builder.Property(f => f.DepreciationAmountByIncomeTax);

        builder.Property(f => f.RefNo)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(f => f.RefNo)
            .IsUnique();

        builder.Property(f => f.FixedAssetCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(f => f.FixedAssetName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(f => f.Manufacturer)
            .HasMaxLength(200);

        builder.Property(f => f.OrgPriceAccount)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.MadeIn)
            .HasMaxLength(100);

        builder.Property(f => f.DepreciationAccount)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.Source)
            .HasMaxLength(200);

        builder.Property(f => f.CapacityMachine)
            .HasMaxLength(100);

        builder.Property(f => f.SerialNumber)
            .HasMaxLength(100);

        builder.Property(f => f.VendorName)
            .HasMaxLength(200);

        builder.Property(f => f.GuaranteeDuration)
            .HasMaxLength(100);

        builder.Property(f => f.GuaranteeCondition)
            .HasMaxLength(200);

        builder.Property(f => f.DeliveryRecordNo)
            .HasMaxLength(100);

        builder.Property(f => f.OrganizationUnitCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.OrganizationUnitName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(f => f.FixedAssetCategoryName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(f => f.LifeTimeInMonth);

        builder.Property(f => f.LifeTimeRemainingInMonth);

        builder.Property(f => f.DepreciationRateYear);

        builder.Property(f => f.FixedAssetState)
            .HasConversion<int>();

        builder.Property(f => f.FixedAssetCategoryCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.BranchName)
            .HasMaxLength(200);

        builder.Property(f => f.ReasonNotDepreciation)
            .HasMaxLength(200);

        builder.Property(f => f.IsMappingSmart)
            .HasDefaultValue(false);

        builder.Property(f => f.ExcelRowIndex)
            .HasDefaultValue(0);

        builder.Property(f => f.IsValid)
            .HasDefaultValue(false);

        builder.Property(f => f.RefType)
            .IsRequired();

        builder.Property(f => f.AttachmentIdList);

        builder.Property(f => f.CreatedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(f => f.CreatedBy)
            .HasMaxLength(100);

        builder.Property(f => f.ModifiedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(f => f.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(f => f.AutoRefNo)
            .HasDefaultValue(false);

        builder.Property(f => f.ForceUpdate)
            .HasDefaultValue(true);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.State)
            .HasDefaultValue(0);

        builder.Property(f => f.LocationWarehouseId);

        builder.Property(f => f.LocationWarehouseCode);

        builder.Property(f => f.LocationWarehouseName);

        builder.HasMany(f => f.FixedAssetDetails)
            .WithOne()
            .HasForeignKey("FixedAssetId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.FixedAssetDetailAllocations)
            .WithOne()
            .HasForeignKey("FixedAssetId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.FixedAssetDetailSources)
            .WithOne()
            .HasForeignKey("FixedAssetId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.FixedAssetDetailAccessories)
            .WithOne()
            .HasForeignKey("FixedAssetId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
