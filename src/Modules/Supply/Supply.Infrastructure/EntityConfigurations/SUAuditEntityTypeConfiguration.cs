namespace Supply.Infrastructure.EntityConfigurations;

public class SUAuditEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAudit>
{
    public void Configure(EntityTypeBuilder<SUAudit> builder)
    {
        builder.ToTable("SUAudit");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefId")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.TenantId);

        builder.Property(x => x.BranchId);

        builder.Property(x => x.RefType)
            .IsRequired();

        builder.Property(x => x.DisplayOnBook);

        builder.Property(x => x.RefDate);

        builder.Property(x => x.RefTime);

        builder.Property(x => x.BalanceDate);

        builder.Property(x => x.CreatedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.ModifiedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.IsExecuted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.RefNo)
            .HasMaxLength(50);

        builder.HasIndex(x => x.RefNo)
            .IsUnique();

        builder.Property(x => x.JournalMemo)
            .HasMaxLength(255);

        builder.Property(x => x.Summary)
            .HasMaxLength(255);

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(100);

        builder.Property(x => x.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(x => x.State)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.AuditMember)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.BranchName)
            .HasMaxLength(200);

        builder.Property(x => x.AttachmentIdList)
            .HasColumnType("nvarchar(max)");

        builder.HasMany(x => x.SUAuditDetails)
            .WithOne()
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
