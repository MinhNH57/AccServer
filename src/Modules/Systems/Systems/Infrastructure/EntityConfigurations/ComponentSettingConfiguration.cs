using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Systems.Infrastructure.Entities;

namespace Systems.Infrastructure.EntityConfigurations;

public class ComponentSettingConfiguration : IEntityTypeConfiguration<ComponentSetting>
{
    public void Configure(EntityTypeBuilder<ComponentSetting> builder)
    {
        builder.ToTable("ComponentSettings");
        builder.HasKey(e => e.ComponentCode);
        builder.HasMany(e => e.ComponentProperties)
            .WithOne()
            .HasForeignKey(e => e.ComponentCode)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasMany(e => e.ContextMenuProperties)
            .WithOne()
            .HasForeignKey(e => e.ComponentCode)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
