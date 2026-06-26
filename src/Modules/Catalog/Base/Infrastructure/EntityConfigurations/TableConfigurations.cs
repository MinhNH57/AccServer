using Catalog.Base.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Base.Infrastructure.EntityConfigurations;

public class CatalogAccountSymbolConfigurations : IEntityTypeConfiguration<CatalogAccountSymbol>
{
    public void Configure(EntityTypeBuilder<CatalogAccountSymbol> builder)
    {
        builder.ToTable("CatalogAccountSymbol", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.AccountSymbol);
    }
}
public class CatalogRoomConfigurations : IEntityTypeConfiguration<CatalogRoom>
{
    public void Configure(EntityTypeBuilder<CatalogRoom> builder)
    {
        builder.ToTable("CatalogRoom", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.CodeRoom);
    }
}
