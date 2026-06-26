namespace Supply.Infrastructure.EntityConfigurations;

public class SUAdjustmentDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<SUAdjustmentDetail>
{
    public void Configure(EntityTypeBuilder<SUAdjustmentDetail> builder)
    {
        builder.ToTable("SUAdjustmentDetail");

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

        builder.Property(x => x.Quantity);

        builder.Property(x => x.AllocationAccount);

        builder.Property(x => x.CurrentRemainingAmount);

        builder.Property(x => x.NewRemainingAmount);

        builder.Property(x => x.DiffRemainingAmount);

        builder.Property(x => x.CurrentRemainingAllocationTime);

        builder.Property(x => x.NewRemainingAllocationTime);

        builder.Property(x => x.DiffAllocationTime);

        builder.Property(x => x.TermlyAllocationAmount);

        builder.Property(x => x.Note);

        builder.Property(x => x.SortOrder)
         .IsRequired();

        builder.Property(x => x.State)
         .IsRequired()
         .HasDefaultValue(0);

        builder.Property(x => x.EditVersion);

        builder.HasOne<SUAdjustment>()
            .WithMany(r => r.SUAdjustmentDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
