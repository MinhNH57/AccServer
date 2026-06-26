using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SalesSmartContentsDataConfiguration : IEntityTypeConfiguration<SalesSmartContentsData>
{
    public void Configure(EntityTypeBuilder<SalesSmartContentsData> builder)
    {
        builder.ToTable("SalesSmartContentsData");
        builder.HasKey(e => e.IdSource);
        //builder.HasOne<SalesSmartData>()
        //    .WithMany(e => e.SalesSmartContentsDatas)
        //    .HasForeignKey(e => e.IdContents)
        //    .IsRequired();
        //builder.Property(e => e.IdAsc)
        //    .UseIdentityColumn()
        //    .ValueGeneratedOnUpdate();
    }
}
