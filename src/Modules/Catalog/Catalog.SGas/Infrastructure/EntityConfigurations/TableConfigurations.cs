using Catalog.SGas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.SGas.Infrastructure.EntityConfigurations;

public class CatalogAccountSymbolConfigurations : IEntityTypeConfiguration<CatalogAccountSymbol>
{
    public void Configure(EntityTypeBuilder<CatalogAccountSymbol> builder)
    {
        builder.ToTable("CatalogAccountSymbol", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.AccountSymbol);
    }
}
