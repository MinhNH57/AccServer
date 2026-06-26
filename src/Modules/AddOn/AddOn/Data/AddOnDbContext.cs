using AddOn.Data.Entities;
using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AddOn.Data;

public class AddOnDbContext : MultiTenantBaseContext
{
    public AddOnDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public AddOnDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<AddOnDbContext> options) : base(multiTenantContextAccessor, options)
    {
    }


    public DbSet<DataBalanceFluctuations> DataBalanceFluctuations => Set<DataBalanceFluctuations>();
    public DbSet<DataControlled> DataControlled => Set<DataControlled>();
    public DbSet<CatalogBankOfAccount> CatalogBankOfAccount => Set<CatalogBankOfAccount>();
    public DbSet<DataThuChi> VTDATATHUCHI => Set<DataThuChi>();
}