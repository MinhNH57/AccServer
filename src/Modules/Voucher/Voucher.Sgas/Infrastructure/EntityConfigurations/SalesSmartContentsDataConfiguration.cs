using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Sgas.Entities;

namespace Voucher.Sgas.Infrastructure.EntityConfigurations;

public class SalesSmartContentsDataConfiguration : IEntityTypeConfiguration<SalesSmartContentsData>
{
    public void Configure(EntityTypeBuilder<SalesSmartContentsData> builder)
    { 
        builder.ToTable("SalesSmartContentsData",tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.IdSource); 
    }
}
