using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogAccountMovement
    {
        public string? AccountSymbol { get; set; }
        public string? DebitSide { get; set; }
        public string? CreditSide { get; set; }
        public int? Ordinal { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public Guid Id { get; set; }
    }
}
