using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Voucher.HRM.Entities;

namespace Voucher.HRM.Infrastructure;
public class VoucherHrmDbContext : MultiTenantBaseContext
{
    private readonly ICurrentUser _currentUser;

    public VoucherHrmDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
    {
        _currentUser = currentUser;

    }

    public VoucherHrmDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<VoucherHrmDbContext> options, ICurrentUser currentUser) : base(multiTenantContextAccessor, options)
    {
        _currentUser = currentUser;
    }
    public DbSet<CatalogTableCommon> CatalogTableCommon { get; set; }
    public DbSet<SmartDataApplication> SmartDataApplication { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoucherHrmDbContext).Assembly);
    }
}
