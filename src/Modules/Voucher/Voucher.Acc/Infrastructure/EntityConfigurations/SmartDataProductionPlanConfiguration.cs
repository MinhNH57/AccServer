using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model.ProductPlant;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartDataProductionPlanConfiguration : IEntityTypeConfiguration<SmartDataProductionPlan>
{
    public void Configure(EntityTypeBuilder<SmartDataProductionPlan> builder)
    {
        builder.ToTable("SmartDataProductionPlan", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.IdSource);
        builder.HasOne<SmartProductionPlan>()
            .WithMany(e => e.SmartDataProductionPlans)
            .HasForeignKey(e => e.IdData)
            .IsRequired();
    }
}
