using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class CreditContractContentsConfiguration : IEntityTypeConfiguration<CreditContractContents>
{
    public void Configure(EntityTypeBuilder<CreditContractContents> builder)
    {
        builder.ToTable("CreditContractContents", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.IdSource);
        builder.HasOne<CreditContract>()
            .WithMany(e => e.CreditContractContents)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
    }
}
