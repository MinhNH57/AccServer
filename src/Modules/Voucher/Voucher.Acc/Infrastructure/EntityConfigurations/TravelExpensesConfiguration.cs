using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class TravelExpensesConfiguration : IEntityTypeConfiguration<TravelExpenses>
{
    public void Configure(EntityTypeBuilder<TravelExpenses> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne<RequiPaymentData>().WithMany(c => c.TravelExpensess).HasForeignKey(c => c.IdContents);
    }
}