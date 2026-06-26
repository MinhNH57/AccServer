using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartDataManufactureConfiguration : IEntityTypeConfiguration<SmartDataManufacture>
{
    public void Configure(EntityTypeBuilder<SmartDataManufacture> builder)
    {
        builder.ToTable("SmartDataManufacture", tb => tb.UseSqlOutputClause(false));
        builder.HasKey(e => e.Id);
        builder.HasMany(e=>e.SmartDataManufactureContents)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        //builder.HasMany(e => e.SalesSmartContentsDatas)
        //    .WithOne()
        //    .HasForeignKey(e => e.IdContents)
        //    .OnDelete(DeleteBehavior.Cascade)
        //    .IsRequired();
        //builder.Property(e => e.IdAsc)
        //    .UseIdentityColumn()
        //    .ValueGeneratedOnUpdate();
    }
}
