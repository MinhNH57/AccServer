using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartVatTaxListConfig:IEntityTypeConfiguration<SmartVatTaxList>
{
    public void Configure(EntityTypeBuilder<SmartVatTaxList> builder)
    {
        builder.ToTable("SmartVatTaxList");
        builder.HasKey(e => e.IdSource);
        builder.HasOne<SmartData>()
            .WithMany(e => e.SmartVatTaxLists)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
    }
}