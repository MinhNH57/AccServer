using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartPaymentVendorConfig : IEntityTypeConfiguration<SmartPaymentVendor>
{
    public void Configure(EntityTypeBuilder<SmartPaymentVendor> builder)
    {
        builder.ToTable("SmartPaymentVendor");
        builder.HasKey(e => e.IdSource);
        builder.HasOne<SmartData>()
            .WithMany(e => e.SmartPaymentVendors)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
        builder.HasOne<CreditContract>()
           .WithMany(e => e.SmartPaymentVendors)
           .HasForeignKey(e => e.IdContents)
           .IsRequired();
    }
}