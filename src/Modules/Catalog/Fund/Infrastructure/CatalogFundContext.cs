using BuildingBlocks.MultiTenancy;
using Catalog.Fund.Entities;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Infrastructure;

public partial class CatalogFundContext: MultiTenantBaseContext
{
    public CatalogFundContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public CatalogFundContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<CatalogFundContext> options) : base(multiTenantContextAccessor, options)
    {
    }

    public DbSet<CatalogObject> CatalogObject => Set<CatalogObject>();
    public DbSet<CatalogRelationship> CatalogRelationship => Set<CatalogRelationship>();
    public DbSet<SurveyExpertise> SurveyExpertise  => Set<SurveyExpertise>();
    public DbSet<ExcelCatalogObject> ExcelCatalogObject => Set<ExcelCatalogObject>();


}