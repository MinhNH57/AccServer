using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ref.API.Model;

namespace Ref.API.Infrastructure.EntityConfigurations;

public class ViewReferenceEntityTypeConfiguration : IEntityTypeConfiguration<ViewReference>
{
    public void Configure(EntityTypeBuilder<ViewReference> builder)
    {
        builder.ToTable("ViewReference");

        builder.HasKey(x => x.RefId);

        builder.HasIndex(x => new { x.RefType, x.RefDate });
        builder.HasIndex(x => x.SessionId);

        builder.Property(x => x.RefNoFinance)
               .HasMaxLength(50);

        builder.Property(x => x.RefNoManagement)
               .HasMaxLength(50);

        builder.Property(x => x.JournalMemo)
               .HasMaxLength(512);

        builder.Property(x => x.AccountObjectCode)
               .HasMaxLength(50);

        builder.Property(x => x.AccountObjectName)
               .HasMaxLength(255);

        builder.Property(x => x.DebitAccount)
               .HasMaxLength(50);

        builder.Property(x => x.CreditAccount)
               .HasMaxLength(50);

        builder.Property(x => x.TotalAmount);

        builder.Property(x => x.RefDate);

        builder.Property(x => x.PostedDate);

        builder.Property(x => x.RefType);

        builder.Property(x => x.RowNum);
    }
}
