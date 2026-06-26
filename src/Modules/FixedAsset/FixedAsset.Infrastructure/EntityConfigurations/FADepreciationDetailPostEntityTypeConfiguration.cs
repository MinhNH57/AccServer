namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FADepreciationDetailPostEntityTypeConfiguration
    : IEntityTypeConfiguration<FADepreciationDetailPost>
{
    public void Configure(EntityTypeBuilder<FADepreciationDetailPost> builder)
    {
        builder.ToTable("FADepreciationDetailPost");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.TenantId);

        builder.Property(x => x.FixedAssetId);

        builder.Property(x => x.DebitAccountObjectId);

        builder.Property(x => x.CreditAccountObjectId);

        builder.Property(x => x.ExpenseItemId);

        builder.Property(x => x.OrganizationUnitId);

        builder.Property(x => x.JobId);

        builder.Property(x => x.ProjectWorkId);

        builder.Property(x => x.OrderId);

        builder.Property(x => x.ContractId);

        builder.Property(x => x.ListItemId);

        builder.Property(x => x.SortOrder)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.UnReasonableCost)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasDefaultValue(0.0);

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.Property(x => x.DebitAccount)
            .HasMaxLength(50);

        builder.Property(x => x.CreditAccount)
            .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
            .HasMaxLength(250);

        builder.Property(x => x.ListItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.ExpenseItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitCode)
            .HasMaxLength(50);

        builder.Property(x => x.JobCode)
            .HasMaxLength(50);

        builder.Property(x => x.JobName)
            .HasMaxLength(250);

        builder.Property(x => x.ExpenseItemName)
            .HasMaxLength(250);

        builder.Property(x => x.ListItemName)
            .HasMaxLength(250);

        builder.Property(x => x.ProjectWorkCode)
            .HasMaxLength(50);

        builder.Property(x => x.CreditAccountObjectCode)
            .HasMaxLength(50);

        builder.Property(x => x.DebitAccountObjectCode)
            .HasMaxLength(50);

        builder.Property(x => x.CreditAccountObjectName)
            .HasMaxLength(250);

        builder.Property(x => x.DebitAccountObjectName)
            .HasMaxLength(250);

        builder.Property(x => x.OrderCode)
            .HasMaxLength(50);

        builder.Property(x => x.ContractCode)
            .HasMaxLength(50);

        builder.Property(x => x.ContractSubject)
            .HasMaxLength(250);

        builder.Property(x => x.ProjectWorkName)
            .HasMaxLength(250);

        builder.Property(x => x.AccountName)
            .HasMaxLength(250);

        builder.Property(x => x.ContractSignDate);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.AllocationObjectType);

        builder.HasOne<FADepreciation>()
            .WithMany(r => r.FADepreciationDetailPosts)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
