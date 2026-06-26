using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Sgas.Entities;

namespace Voucher.Sgas.Infrastructure.EntityConfigurations;

public class SalesSmartProductInventoryConfiguration : IEntityTypeConfiguration<SalesSmartProductInventory>
{
    public void Configure(EntityTypeBuilder<SalesSmartProductInventory> builder)
    {
        builder.ToTable("SalesSmartProductInventory", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.Id);
    }
}