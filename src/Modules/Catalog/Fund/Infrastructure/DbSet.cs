using Catalog.Fund.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Infrastructure;

public partial class CatalogFundContext
{
    #region Fund

    //public DbSet<CatalogRelationship> CatalogRelationship => Set<CatalogRelationship>();
    //public DbSet<SurveyExpertise> SurveyExpertise => Set<SurveyExpertise>();
    public DbSet<CatalogVillage> CatalogVillage { get; set; }

    #endregion
}