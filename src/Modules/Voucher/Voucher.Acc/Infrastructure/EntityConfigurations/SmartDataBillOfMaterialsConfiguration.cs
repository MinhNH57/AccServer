using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartDataBillOfMaterialsConfiguration : IEntityTypeConfiguration<SmartDataBillOfMaterials>
{
    public void Configure(EntityTypeBuilder<SmartDataBillOfMaterials> builder)
    {
        builder.ToTable("SmartDataBillOfMaterials");
        builder.HasKey(e => e.IdSource);
        builder.HasOne<SmartDataManufacture>()
            .WithMany(e => e.SmartDataBillOfMaterials)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
    }
}
