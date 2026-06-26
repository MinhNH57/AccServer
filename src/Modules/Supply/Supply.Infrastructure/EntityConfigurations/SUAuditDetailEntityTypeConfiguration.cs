namespace Supply.Infrastructure.EntityConfigurations;

public class SUAuditDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAuditDetail>
{
    public void Configure(EntityTypeBuilder<SUAuditDetail> builder)
    {
        builder.ToTable("SUAuditDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.SupplyId);


        builder.Property(x => x.SupplyCode)
            .HasMaxLength(50);

        builder.Property(x => x.SupplyName)
            .HasMaxLength(200);

        builder.Property(x => x.OrganizationUnitId);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.Property(x => x.Action);

        builder.Property(x => x.QuantityOnBook);

        builder.Property(x => x.QuantityInventory);

        builder.Property(x => x.DiffQuantity);

        builder.Property(x => x.GoodQuantity);

        builder.Property(x => x.DamageQuantity);

        builder.Property(x => x.ExecuteQuantity);

        builder.Property(x => x.Note)
            .IsRequired();

        builder.Property(x => x.OrganizationUnitCode)
            .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
            .HasMaxLength(200);


        builder.Property(x => x.State)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.Property(x => x.Unit);

        builder.HasOne<SUAudit>()
            .WithMany(r => r.SUAuditDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
