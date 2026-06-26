namespace FixedAsset.Infrastructure.EntityConfigurations;

public class FAAuditDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<FAAuditDetail>
{
    public void Configure(EntityTypeBuilder<FAAuditDetail> builder)
    {
        builder.ToTable("FAAuditDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.FixedAssetId);

        builder.Property(x => x.OrganizationUnitId);

        builder.Property(x => x.ExistInStock)
         .IsRequired();

        builder.Property(x => x.Quality)
         .IsRequired();

        builder.Property(x => x.SortOrder)
         .IsRequired();

        builder.Property(x => x.OrgPrice)
         .IsRequired();

        builder.Property(x => x.DepreciationAmount)
         .IsRequired();

        builder.Property(x => x.AccumDepreciationAmount)
         .IsRequired();

        builder.Property(x => x.RemainingAmount)
         .IsRequired();

        builder.Property(x => x.Note)
         .IsRequired();

        builder.Property(x => x.OrganizationUnitCode)
         .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
         .HasMaxLength(200);


        builder.Property(x => x.FixedAssetCode)
         .HasMaxLength(50);

        builder.Property(x => x.FixedAssetName)
         .HasMaxLength(200);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.HasOne<FAAudit>()
            .WithMany(r => r.FAAuditDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
