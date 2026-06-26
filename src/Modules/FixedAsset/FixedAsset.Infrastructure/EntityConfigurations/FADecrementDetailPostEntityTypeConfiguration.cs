namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FADecrementDetailPostEntityTypeConfiguration
    : IEntityTypeConfiguration<FADecrementDetailPost>
{
    public void Configure(EntityTypeBuilder<FADecrementDetailPost> builder)
    {
        builder.ToTable("FADecrementDetailPost");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.ExpenseItemId);
        builder.Property(x => x.ExpenseItemCode).HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitId);
        builder.Property(x => x.OrganizationUnitCode).HasMaxLength(50);

        builder.Property(x => x.JobId);
        builder.Property(x => x.JobCode).HasMaxLength(50);

        builder.Property(x => x.ProjectWorkId);
        builder.Property(x => x.ProjectWorkCode).HasMaxLength(50);

        builder.Property(x => x.OrderId);
        builder.Property(x => x.OrderCode).HasMaxLength(50);

        builder.Property(x => x.ContractId);
        builder.Property(x => x.ContractCode).HasMaxLength(50);

        builder.Property(x => x.ListItemId);
        builder.Property(x => x.ListItemCode).HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName).HasMaxLength(200);
        builder.Property(x => x.JobName).HasMaxLength(200);
        builder.Property(x => x.ProjectWorkName).HasMaxLength(200);
        builder.Property(x => x.ExpenseItemName).HasMaxLength(200);
        builder.Property(x => x.ListItemName).HasMaxLength(200);

        builder.Property(x => x.AccountObjectId);
        builder.Property(x => x.AccountObjectCode).HasMaxLength(50);
        builder.Property(x => x.AccountObjectName).HasMaxLength(200);

        builder.Property(x => x.SortOrder).IsRequired();

        builder.Property(x => x.UnReasonableCost)
         .IsRequired()
         .HasDefaultValue(false);

        builder.Property(x => x.Amount)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.Description)
         .HasMaxLength(512)
         .IsRequired();

        builder.Property(x => x.DebitAccount)
         .HasMaxLength(50);

        builder.Property(x => x.CreditAccount)
         .HasMaxLength(50);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(1);

        builder.HasOne<FADecrement>()
        .WithMany(r => r.FADecrementDetailPosts)
        .HasForeignKey("RefId")
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
    }
}