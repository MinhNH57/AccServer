namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FAChangeFinancialLeasingToOwnerDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<FAChangeFinancialLeasingToOwnerDetail>
{
    public void Configure(EntityTypeBuilder<FAChangeFinancialLeasingToOwnerDetail> builder)
    {
        builder.ToTable("FAChangeFinancialLeasingToOwnerDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.SortOrder);

        builder.Property(x => x.Description)
         .HasMaxLength(512);

        builder.Property(x => x.DebitAccount)
            .HasMaxLength(100);

        builder.Property(x => x.CreditAccount)
            .HasMaxLength(100);

        builder.Property(x => x.Amount);

        builder.Property(x => x.ListItemId);

        builder.Property(x => x.ListItemCode);

        builder.Property(x => x.ListItemName);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.HasOne<FAChangeFinancialLeasingToOwner>()
            .WithMany(r => r.FAChangeFinancialLeasingToOwnerDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
