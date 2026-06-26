using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities
{
    public class CatalogAccountMovement
    {
        public string? AccountSymbol { get; set; }
        public string? AccountSymbolName { get; set; }
        public string? AccountMovementTypeCode { get; set; }
        public string? AccountMovementTypeName { get; set; }
        public string? DebitSide { get; set; }
        public string? CreditSide { get; set; }
        public int? Ordinal { get; set; }
        public string? TransferSide { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
