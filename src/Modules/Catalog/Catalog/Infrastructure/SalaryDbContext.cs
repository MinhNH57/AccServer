using Catalog.Base.Entities;
using Finbuckle.MultiTenant.Abstractions;

namespace Catalog.Infrastructure;

public class SalaryDbContext:MultiTenantBaseContext
{
    public SalaryDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public SalaryDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<SalaryDbContext> options) : base(multiTenantContextAccessor, options)
    {
    }

    public DbSet<SalarySummaryOfTimekeeping> SalarySummaryOfTimekeeping => Set<SalarySummaryOfTimekeeping>();
    public DbSet<SalaryTimeSheet> SalaryTimeSheet => Set<SalaryTimeSheet>();
    public DbSet<SalaryTimeSheetDetail> SalaryTimeSheetDetail => Set<SalaryTimeSheetDetail>();
    public DbSet<SalaryTimeSheetSummary> SalaryTimeSheetSummary => Set<SalaryTimeSheetSummary>();
    public DbSet<SalaryTimeSheetSummaryDetails> SalaryTimeSheetSummaryDetails => Set<SalaryTimeSheetSummaryDetails>();
}