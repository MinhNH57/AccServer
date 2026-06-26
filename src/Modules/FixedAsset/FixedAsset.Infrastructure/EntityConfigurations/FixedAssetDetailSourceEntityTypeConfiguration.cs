namespace FixedAsset.Infrastructure.EntityConfigurations;

internal class FixedAssetDetailSourceEntityTypeConfiguration
    : IEntityTypeConfiguration<FixedAssetDetailSource>
{
    public void Configure(EntityTypeBuilder<FixedAssetDetailSource> builder)
    {
        builder.ToTable("FixedAssetDetailSource");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("FixedAssetDetailId")
            .IsRequired();

        builder.Property(f => f.RefId);

        builder.Property(f => f.RefDetailId);

        builder.Property(f => f.SortOrder);

        builder.Property(f => f.RefType);

        builder.Property(f => f.JournalMemo)
            .HasMaxLength(250);

        builder.Property(f => f.CreditAccount)
            .HasMaxLength(100);

        builder.Property(f => f.DebitAccount)
            .HasMaxLength(100);

        builder.Property(f => f.RefNo)
            .HasMaxLength(100);

        builder.Property(f => f.Amount);

        builder.Property(f => f.RefDate);

        builder.Property(f => f.PostedDate);

        builder.Property(f => f.DetailPostOrder);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.State)
           .HasDefaultValue(0);

        builder.HasOne<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset>()
            .WithMany(r => r.FixedAssetDetailSources)
            .HasForeignKey("FixedAssetId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
