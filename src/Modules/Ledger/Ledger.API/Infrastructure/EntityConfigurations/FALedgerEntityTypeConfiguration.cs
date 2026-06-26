using Ledger.API.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.API.Infrastructure.EntityConfigurations;

internal class FALedgerEntityTypeConfiguration : IEntityTypeConfiguration<FALedger>
{
    public void Configure(EntityTypeBuilder<FALedger> builder)
    {
        builder.ToView("ViewFALedger");
        builder.HasKey(f => f.FixedAssetId);
    }
}