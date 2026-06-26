
using Financial.Model;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigureFinancial(this ModelBuilder modelBuilder, string defaultSchema)
    {
        if (!string.IsNullOrWhiteSpace(defaultSchema))
        {
            modelBuilder.HasDefaultSchema(defaultSchema);
        }

        modelBuilder.Entity<ReportBalanceSheet>(client =>
        {
            client.ToTable("ReportBalanceSheet");
            client.HasKey(x => x.Id);
            client.Property(x => x.Id).ValueGeneratedOnAdd();
        });

        return modelBuilder;
    }
}