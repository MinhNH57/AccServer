namespace Supply.Infrastructure.EntityConfigurations;

public class SUTransferDetailEntityTypeConfiguration
    : IEntityTypeConfiguration<SUTransferDetail>
{
    public void Configure(EntityTypeBuilder<SUTransferDetail> builder)
    {
        builder.ToTable("SUTransferDetail");

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

        builder.Property(x => x.FromOrganizationUnitId);

        builder.Property(x => x.FromOrganizationUnitCode)
            .HasMaxLength(50);

        builder.Property(x => x.FromOrganizationUnitName)
            .HasMaxLength(200);

        builder.Property(x => x.ToOrganizationUnitId);

        builder.Property(x => x.ToOrganizationUnitCode)
            .HasMaxLength(50);

        builder.Property(x => x.ToOrganizationUnitName)
            .HasMaxLength(200);

        builder.Property(x => x.ListItemId);

        builder.Property(x => x.ListItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.ListItemName)
            .HasMaxLength(200);

        builder.Property(x => x.ContractId);

        builder.Property(x => x.ContractCode)
            .HasMaxLength(50);

        builder.Property(x => x.OrderId);

        builder.Property(x => x.OrderCode)
            .HasMaxLength(50);

        builder.Property(x => x.ProjectWorkId);

        builder.Property(x => x.ProjectWorkCode)
            .HasMaxLength(50);

        builder.Property(x => x.ProjectWorkName)
            .HasMaxLength(200);

        builder.Property(x => x.ExpenseItemId);

        builder.Property(x => x.ExpenseItemCode)
            .HasMaxLength(50);

        builder.Property(x => x.ExpenseItemName)
            .HasMaxLength(200);

        builder.Property(x => x.JobId);

        builder.Property(x => x.JobCode)
            .HasMaxLength(50);

        builder.Property(x => x.JobName)
            .HasMaxLength(200);

        builder.Property(x => x.SortOrder)
         .IsRequired();

        builder.Property(x => x.UseQuantity);

        builder.Property(x => x.TransferQuantity);

        builder.Property(x => x.CostAccount)
            .HasMaxLength(50);

        builder.HasOne<SUTransfer>()
            .WithMany(r => r.SUTransferDetails)
            .HasForeignKey("RefId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
