namespace Supply.Infrastructure.EntityConfigurations;

internal class SUIncrementDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<SUIncrementDetail>
{
    public void Configure(EntityTypeBuilder<SUIncrementDetail> builder)
    {
        builder.ToTable("SupplyDetail");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("SupplyDetailId")
            .IsRequired();

        builder.Property(f => f.SortOrder)
            .IsRequired();

        builder.Property(f => f.Description)
            .HasMaxLength(250);

        builder.Property(f => f.NumberNo)
            .HasMaxLength(100);

        builder.Property(f => f.State)
            .HasDefaultValue(0);

        builder.Property(f => f.EditVersion);

        builder.HasOne<SUIncrement>()
            .WithMany(r => r.SUIncrementDetails)
            .HasForeignKey("SupplyId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
