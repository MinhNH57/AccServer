using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class RequiPaymentDataContentsConfig : IEntityTypeConfiguration<RequiPaymentDataContents>
{
    public void Configure(EntityTypeBuilder<RequiPaymentDataContents> builder)
    {
        builder.HasKey(c => c.IdSource);
        builder.HasOne<RequiPaymentData>()
            .WithMany(c => c.DataContentsList)
            .HasForeignKey(a => a.IdContents);
    }
}