using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class RequiPaymentDataConfig : IEntityTypeConfiguration<RequiPaymentData>
{
    public void Configure(EntityTypeBuilder<RequiPaymentData> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(c => c.DataContentsList).WithOne().HasForeignKey(a => a.IdContents);
        builder.HasMany(c => c.HeadInvoiceInputs).WithOne().HasForeignKey(a => a.IdRqPayment);
        builder.HasMany(c => c.PaymentPlanContents).WithOne().HasForeignKey(a => a.IdContent);
        builder.HasMany(c => c.RequirePaymentMoneys).WithOne().HasForeignKey(a => a.IdContents);
        builder.HasMany(c => c.TravelExpensess).WithOne().HasForeignKey(a => a.IdContents);
    }
}