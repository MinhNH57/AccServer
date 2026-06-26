namespace Catalog.Base.Entities;
public class CatalogJob
{
    public string? CodeWork { get; set; }

    public string? NameWork { get; set; }

    public string? DataName { get; set; }
    public string? DataType { get; set; }

    public string? ObjCode { get; set; }

    public string? ObjName { get; set; }

    public string? CodePosition { get; set; }

    public string? NamePosition { get; set; }

    public string? CodeRoom { get; set; }

    public string? NameRoom { get; set; }

    public string? Notes { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();
     
    public int? CodeUnit { get; set; }

    public bool IsActive { get; set; }
    public bool IsTaxAccounting { get; set; } = false;
    public bool IsAccountsPayable { get; set; } = false;

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }
}
