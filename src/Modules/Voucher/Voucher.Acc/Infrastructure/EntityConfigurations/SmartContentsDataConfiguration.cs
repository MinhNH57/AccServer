using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartContentsDataConfiguration : IEntityTypeConfiguration<SmartContentsData>
{
    public void Configure(EntityTypeBuilder<SmartContentsData> builder)
    {
        builder.ToTable("SmartContentsData");
        builder.HasKey(e => e.IdSource);
        builder.HasOne<SmartData>()
            .WithMany(e => e.SmartContentsDatas)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
        //builder.Property(e => e.IdAsc)
        //    .UseIdentityColumn()
        //    .ValueGeneratedOnUpdate();
    }
}
