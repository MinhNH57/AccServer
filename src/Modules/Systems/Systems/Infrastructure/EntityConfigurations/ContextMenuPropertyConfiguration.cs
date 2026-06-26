using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Systems.Infrastructure.Entities;

namespace Systems.Infrastructure.EntityConfigurations;

public class ContextMenuPropertyConfiguration : IEntityTypeConfiguration<ContextMenuProperty>
{
    public void Configure(EntityTypeBuilder<ContextMenuProperty> builder)
    {
        builder.ToTable("ContextMenuProperties");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.MenuCode).IsRequired();
        builder.HasOne<ComponentSetting>()
            .WithMany(e => e.ContextMenuProperties)
            .HasForeignKey(e => e.ComponentCode)
            .IsRequired();
    }
}
