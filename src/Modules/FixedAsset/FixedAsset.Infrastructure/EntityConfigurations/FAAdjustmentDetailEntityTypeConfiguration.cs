namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FAAdjustmentDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<FAAdjustmentDetail>
{
    public void Configure(EntityTypeBuilder<FAAdjustmentDetail> builder)
    {
        builder.ToTable("FAAdjustmentDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.FixedAssetId);

        builder.Property(x => x.FixedAssetCode)
         .HasMaxLength(50);

        builder.Property(x => x.FixedAssetName)
         .HasMaxLength(200);

        builder.Property(x => x.OrganizationUnitId);

        builder.Property(x => x.SortOrder)
         .IsRequired();

        builder.Property(x => x.CurrentRemainingAmount)
         .IsRequired();

        builder.Property(x => x.NewRemainingAmount)
         .IsRequired();

        builder.Property(x => x.DiffRemainingAmount)
         .IsRequired();

        builder.Property(x => x.CurrentLifeTime)
         .IsRequired();

        builder.Property(x => x.NewLifeTime)
         .IsRequired();

        builder.Property(x => x.CurrentAccumDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.NewMonthlyDepreciationAmountByIncomeTax)
         .IsRequired();

        builder.Property(x => x.DiffLifeTime)
         .IsRequired();

        builder.Property(x => x.DiffMonthlyDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.NewAccumDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.DiffAccumDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.CurrentDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.NewDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.DiffDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.NewMonthlyDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.CostAccount)
         .HasMaxLength(50);

        builder.Property(x => x.AdjustmentAccount)
         .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitCode)
         .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
         .HasMaxLength(200);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.HasOne<FAAdjustment>()
            .WithMany(r => r.FAAdjustmentDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
