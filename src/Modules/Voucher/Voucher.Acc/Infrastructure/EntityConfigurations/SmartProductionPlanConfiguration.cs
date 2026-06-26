using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model.ProductPlant;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartProductionPlanConfiguration : IEntityTypeConfiguration<SmartProductionPlan>
{
    public void Configure(EntityTypeBuilder<SmartProductionPlan> builder)
    {
        builder.ToTable("SmartProductionPlan", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.SmartDataProductionPlans)
            .WithOne()
            .HasForeignKey(e => e.IdData)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

    }
}
