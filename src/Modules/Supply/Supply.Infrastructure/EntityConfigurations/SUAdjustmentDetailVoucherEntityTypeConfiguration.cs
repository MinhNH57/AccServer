namespace Supply.Infrastructure.EntityConfigurations;

public class SUAdjustmentDetailVoucherEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAdjustmentDetailVoucher>
{
    public void Configure(EntityTypeBuilder<SUAdjustmentDetailVoucher> builder)
    {
        builder.ToTable("SUAdjustmentDetailVoucher");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("RefDetailId")
            .IsRequired();

        builder.Property(x => x.TenantId);

        builder.Property(f => f.VoucherRefId);

        builder.Property(f => f.VoucherRefDetailId);

        builder.Property(f => f.CreditAccount)
             .HasMaxLength(50);

        builder.Property(f => f.DebitAccount)
            .HasMaxLength(50);

        builder.Property(f => f.VoucherRefType);

        builder.Property(f => f.SortOrder)
            .IsRequired();

        builder.Property(f => f.RefNo)
            .HasMaxLength(50);

        builder.Property(f => f.Amount);

        builder.Property(f => f.RefDate);

        builder.Property(f => f.RefTypeName)
            .HasMaxLength(250);

        builder.Property(f => f.Description)
            .HasMaxLength(250);

        builder.Property(f => f.State)
           .HasDefaultValue(0);

        builder.Property(f => f.EditVersion);

        builder.Property(f => f.DetailPostOrder);

        builder.HasOne<SUAdjustment>()
        .WithMany(r => r.SUAdjustmentDetailVouchers)
        .HasForeignKey("RefId")
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
    }
}