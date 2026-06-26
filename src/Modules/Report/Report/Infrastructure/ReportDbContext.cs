using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure.Views;

namespace Report.Infrastructure;

public class ReportDbContext : MultiTenantBaseContext
{
    public ReportDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public ReportDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions options) : base(multiTenantContextAccessor, options)
    {
    }

    public DbSet<FunRepaymentPlan> FunRepaymentPlan => Set<FunRepaymentPlan>();
    public DbSet<SmartFundContract> SmartFundContract => Set<SmartFundContract>();
    public DbSet<FundCreditContractContents> FundCreditContractContents => Set<FundCreditContractContents>();
}