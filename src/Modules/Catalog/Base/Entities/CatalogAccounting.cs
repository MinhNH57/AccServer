using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogAccounting
{
    [MaxLength(50)]
    public string? DataType { get; set; }

    public int? CodeUnit { get; set; }

    [MaxLength(50)]
    public string? WarehoseCode { get; set; }
    [MaxLength(250)]
    public string? WarehoseName { get; set; }

    [MaxLength(20)]
    public string? AccountSymbol { get; set; }

    [MaxLength(20)]
    public string? DebitSide { get; set; }

    [MaxLength(50)]
    public string? DebitObjectCode { get; set; }

    [MaxLength(255)]
    public string? DebitObjectName { get; set; }

    [MaxLength(20)]
    public string? CreditSide { get; set; }

    [MaxLength(50)]
    public string? CreditObjectCode { get; set; }

    [MaxLength(255)]
    public string? CreditObjectName { get; set; }

    [MaxLength(50)]
    public string? RevenueExpenseCode { get; set; }

    [MaxLength(255)]
    public string? RevenueExpenseName { get; set; }

    [MaxLength(50)]
    public string? MethodOfPaymentsCode { get; set; }

    [MaxLength(255)]
    public string? MethodOfPaymentsName { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    public string? ReasonCode { get; set; }

    [MaxLength(150)]
    public string? ReasonName { get; set; }

    [MaxLength(50)]
    public string? DataName { get; set; }

    public string? DataType1 { get; set; }

    public int? CodeUnit1 { get; set; }
    public string? DataName1 { get; set; }
}
