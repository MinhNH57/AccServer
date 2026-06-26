using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salary.Model;

namespace Salary.Infrastructure.EntityConfigurations;

public class SalaryTimeSheetDetailConfig : IEntityTypeConfiguration<SalaryTimeSheetDetail>
{
    public void Configure(EntityTypeBuilder<SalaryTimeSheetDetail> builder)
    {
        builder.ToTable("SalaryTimeSheetDetail");
        builder.HasKey(c => c.Id);
        builder.HasOne< SalaryTimeSheetHead>()
            .WithMany(c=>c.SalaryTimeSheetDetails)
            .HasForeignKey(c=>c.IdSalaryHead).IsRequired();
    }
}
