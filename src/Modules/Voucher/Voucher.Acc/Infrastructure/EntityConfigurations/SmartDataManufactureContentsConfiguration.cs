using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartDataManufactureContentsConfiguration : IEntityTypeConfiguration<SmartDataManufactureContents>
{
    public void Configure(EntityTypeBuilder<SmartDataManufactureContents> builder)
    {
        builder.ToTable("SmartDataManufactureContents");
        builder.HasKey(e => e.IdSource);
        builder.HasOne<SmartDataManufacture>()
            .WithMany(e => e.SmartDataManufactureContents)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
    }
}
