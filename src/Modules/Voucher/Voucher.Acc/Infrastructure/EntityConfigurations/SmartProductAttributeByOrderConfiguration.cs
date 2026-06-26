using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartProductAttributeByOrderConfiguration : IEntityTypeConfiguration<SmartProductAttributeByOrder>
{
    public void Configure(EntityTypeBuilder<SmartProductAttributeByOrder> builder)
    {
        builder.ToTable("SmartProductAttributeByOrder");
        builder.HasKey(e => e.IdSource);

    }
}

