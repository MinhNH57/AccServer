using GenericServices;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogUnit;

public class CatalogUnitVM : ILinkToEntity<Domain.Entity.Catalogs.CatalogUnit>
{
    public Guid Id { get; set; }
    public int CodeUnit { get; set; }
    public string? NameUnit { get; set; }
    public string? Address { get; set; }
    public bool ByBatchNo { get; set; }
    public string? PositionDir { get; set; }
    public string? DirectorName { get; set; }
    public bool IsActive {get;set;}
    public string? Taxcode { get; set; }
}