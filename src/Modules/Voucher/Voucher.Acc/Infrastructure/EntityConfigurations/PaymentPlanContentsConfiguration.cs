using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class PaymentPlanContentsConfiguration: IEntityTypeConfiguration<PaymentPlanContents>
{
    public void Configure(EntityTypeBuilder<PaymentPlanContents> builder)
    {
        builder.HasKey(c=>c.Id);
        builder.HasOne<RequiPaymentData>().WithMany(a=>a.PaymentPlanContents).HasForeignKey(c=>c.IdContent);
    }
}