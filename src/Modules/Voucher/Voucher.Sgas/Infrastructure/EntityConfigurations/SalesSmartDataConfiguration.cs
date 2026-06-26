using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Sgas.Entities;

namespace Voucher.Sgas.Infrastructure.EntityConfigurations;

public class SalesSmartDataConfiguration : IEntityTypeConfiguration<SalesSmartData>
{
    public void Configure(EntityTypeBuilder<SalesSmartData> builder)
    {
        builder.ToTable("SalesSmartData", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.Id); 
    }
}
