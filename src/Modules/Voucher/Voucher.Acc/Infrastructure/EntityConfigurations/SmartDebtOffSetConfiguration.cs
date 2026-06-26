using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model.DebtOffSet;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;
public class SmartDebtOffSetConfiguration : IEntityTypeConfiguration<SmartDebtOffSet>
{
    public void Configure(EntityTypeBuilder<SmartDebtOffSet> builder)
    {
        builder.ToTable("SmartDebtOffSet", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.SmartDebtOffSetContents)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
