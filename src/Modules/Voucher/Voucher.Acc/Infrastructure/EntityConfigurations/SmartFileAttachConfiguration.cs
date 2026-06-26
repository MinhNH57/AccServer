using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voucher.Acc.Model;

namespace Voucher.Acc.Infrastructure.EntityConfigurations;

public class SmartFileAttachConfiguration : IEntityTypeConfiguration<SmartFileAttach>
{
    public void Configure(EntityTypeBuilder<SmartFileAttach> builder)
    {
        builder.ToTable("SmartFileAttach");
        builder.HasKey(e => e.IdAsc);
        builder.HasOne<SmartData>()
            .WithMany(e => e.SmartFileAttaches)
            .HasForeignKey(e => e.IdContents)
            .IsRequired();
        //builder.Property(e => e.IdAsc)
        //    .UseIdentityColumn()
        //    .ValueGeneratedOnUpdate();
    }
}
