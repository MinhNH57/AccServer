using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salary.Model;

namespace Salary.Infrastructure.EntityConfigurations;

public class SalaryTimeSheetHeadConfiguration : IEntityTypeConfiguration<SalaryTimeSheetHead>
{
    public void Configure(EntityTypeBuilder<SalaryTimeSheetHead> builder)
    {
        builder.ToTable("SalaryTimeSheetHead");
        builder.HasKey(x => x.Id);
        builder.HasMany(c => c.SalaryTimeSheetDetails)
            .WithOne()
            .HasForeignKey(c => c.IdSalaryHead)
            .IsRequired();
    }
}
