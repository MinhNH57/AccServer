namespace Catalog.HRM.Entities;

public class HRM_CatalogForeignLanguage
{
    public Guid? Id { get; set; } 

    public string? LanguageCode { get; set; }

    public string? LanguageName { get; set; }

    public bool? Arrange { get; set; }

    public bool? Show { get; set; }

    public bool? PositionLevel { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsActive { get; set; }
}