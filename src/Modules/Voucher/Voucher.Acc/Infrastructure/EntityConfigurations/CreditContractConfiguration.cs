using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class CreditContractConfiguration : IEntityTypeConfiguration<CreditContract>
{
    public void Configure(EntityTypeBuilder<CreditContract> builder)
    {
        builder.ToTable("CreditContract", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.CreditContractContents)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasMany(e => e.SmartContentsDebtRepaymentPlans)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasMany(e => e.SmartPaymentVendors)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
