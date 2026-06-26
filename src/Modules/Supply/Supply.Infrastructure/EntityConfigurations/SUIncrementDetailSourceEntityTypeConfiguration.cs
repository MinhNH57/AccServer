namespace Supply.Infrastructure.EntityConfigurations;

internal class SUIncrementDetailSourceEntityTypeConfiguration
    : IEntityTypeConfiguration<SUIncrementDetailSource>
{
    public void Configure(EntityTypeBuilder<SUIncrementDetailSource> builder)
    {
        builder.ToTable("SupplyDetailSource");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("SupplyDetailId")
            .IsRequired();

        builder.Property(f => f.TenantId);

        builder.Property(f => f.RefId)
            .IsRequired();

        builder.Property(f => f.RefDetailId);

        builder.Property(f => f.OrganizationUnitId);

        builder.Property(f => f.FixedAssetId);

        builder.Property(f => f.RefType)
            .IsRequired();

        builder.Property(f => f.SortOrder)
            .IsRequired();

        builder.Property(f => f.JournalMemo)
            .HasMaxLength(250);

        builder.Property(f => f.CreditAccount)
            .HasMaxLength(50);

        builder.Property(f => f.DebitAccount)
            .HasMaxLength(50);

        builder.Property(f => f.RefNo)
            .HasMaxLength(50);

        builder.Property(f => f.Amount);

        builder.Property(f => f.RefDate);

        builder.Property(f => f.State)
           .HasDefaultValue(0);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.DetailPostOrder);

        builder.HasOne<SUIncrement>()
            .WithMany(r => r.SUIncrementDetailSources)
            .HasForeignKey("SupplyId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
