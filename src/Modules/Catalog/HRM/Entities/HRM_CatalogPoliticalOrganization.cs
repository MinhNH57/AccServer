namespace Catalog.HRM.Entities;

public class HRM_CatalogPoliticalOrganization
{
    public Guid? Id { get; set; }
    public string? PoliticalOrganizationCode { get; set; }
    public string? PoliticalOrganizationName { get; set; }
    public string? PoliticalOrganizationType { get; set; }
    public bool? Arrange { get; set; }
    public bool? Show { get; set; }
    public bool? PositionLevel { get; set; }
    public string? Notes { get; set; }

    public DateTime? CreateDate { get; set; }
    public int? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public int? ModifyBy { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}