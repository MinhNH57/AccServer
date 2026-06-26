using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class RequirePaymentMoneyConfiguration : IEntityTypeConfiguration<RequirePaymentMoney>
{
    public void Configure(EntityTypeBuilder<RequirePaymentMoney> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne<RequiPaymentData>()
            .WithMany(c => c.RequirePaymentMoneys)
            .HasForeignKey(c => c.IdContents);
    }
}