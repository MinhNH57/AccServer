namespace Supply.Infrastructure.EntityConfigurations;

internal class SULedgerEntityTypeConfiguration : IEntityTypeConfiguration<SULedger>
{
    public void Configure(EntityTypeBuilder<SULedger> builder)
    {
        builder.ToView("ViewSULedger");
        builder.HasKey(f => f.SupplyId);
    }
}