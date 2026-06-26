using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartContentsDebtRepaymentPlanConfiguration : IEntityTypeConfiguration<SmartContentsDebtRepaymentPlan>
{
    public void Configure(EntityTypeBuilder<SmartContentsDebtRepaymentPlan> builder)
    {
        builder.ToTable("SmartContentsDebtRepaymentPlan", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.IdSource);
        builder.HasOne<CreditContract>()
            .WithMany(e => e.SmartContentsDebtRepaymentPlans)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
    }
}
