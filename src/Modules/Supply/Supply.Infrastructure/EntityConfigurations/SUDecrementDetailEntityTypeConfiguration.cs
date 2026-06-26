namespace Supply.Infrastructure.EntityConfigurations;

public class SUDecrementDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<SUDecrementDetail>
{
    public void Configure(EntityTypeBuilder<SUDecrementDetail> builder)
    {
        builder.ToTable("SUDecrementDetail");

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

        builder.Property(x => x.SUAllocationId);

        builder.Property(x => x.SUAuditRefId);

        builder.Property(x => x.SortOrder)
         .IsRequired();

        builder.Property(x => x.UseQuantity);

        builder.Property(x => x.DecrementQuantity);

        builder.Property(x => x.DecrementAmount);

        builder.Property(x => x.RemainingDecrementAmount);

        builder.Property(x => x.Reason);

        builder.Property(x => x.OrganizationUnitCode)
         .HasMaxLength(50);

        builder.Property(x => x.OrganizationUnitName)
         .HasMaxLength(200);

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.HasOne<SUDecrement>()
            .WithMany(r => r.SUDecrementDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
