 using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Systems.Infrastructure.Entities;

namespace Systems.Infrastructure.EntityConfigurations;

public class ComponentPropertyConfiguration : IEntityTypeConfiguration<ComponentProperty>
{
    public void Configure(EntityTypeBuilder<ComponentProperty> builder)
    {
        builder.ToTable("ComponentProperties");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FieldCode).IsRequired();
        builder.HasOne<ComponentSetting>()
            .WithMany(e => e.ComponentProperties)
            .HasForeignKey(e => e.ComponentCode)
            .IsRequired();
    }
}