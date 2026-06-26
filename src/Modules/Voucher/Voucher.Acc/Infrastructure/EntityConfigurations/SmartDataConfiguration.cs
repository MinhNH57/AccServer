using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartDataConfiguration : IEntityTypeConfiguration<SmartData>
{
    public void Configure(EntityTypeBuilder<SmartData> builder)
    {
        builder.ToTable("SmartData", tb => tb.UseSqlOutputClause(false));
        
        builder.HasKey(e => e.Id);
        
        builder.HasIndex(c => c.DataType).IsUnique(false);
        
        builder.HasMany(e => e.SmartContentsDatas)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.HasMany(c => c.SmartVatTaxLists)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.HasMany(e => e.SmartFileAttaches)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasMany(e => e.SmartPaymentVendors)
            .WithOne()
            .HasForeignKey(e => e.IdContents)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        //builder.Property(e => e.IdAsc)
        //    .UseIdentityColumn()
        //    .ValueGeneratedOnUpdate();
    }
}