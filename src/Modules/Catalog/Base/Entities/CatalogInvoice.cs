using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogInvoice
{
    [MaxLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? ContractNumber { get; set; }

    [MaxLength(255)]
    public string? ObjectCode { get; set; }

    [MaxLength(255)]
    public string? ObjectName { get; set; }

    [MaxLength(50)]
    public string? TaxCode { get; set; }

    [MaxLength(100)]
    public string? Status { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; } = 0;

    public int? CodeUnit { get; set; }

    public bool IsActive { get; set; } = true;

}
