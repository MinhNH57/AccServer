using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;
using Voucher.Acc.Model.DebtOffSet;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;
public class SmartDebtOffSetContentsConfiguration : IEntityTypeConfiguration<SmartDebtOffSetContents>
{
    public void Configure(EntityTypeBuilder<SmartDebtOffSetContents> builder)
    {
        builder.ToTable("SmartDebtOffSetContents", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.IdSource);
        builder.HasOne<SmartDebtOffSet>()
            .WithMany(e => e.SmartDebtOffSetContents)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
    }
}
