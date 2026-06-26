using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities
{
    public class CatalogAccountCostSold
    {
        public string? AccountSymbol { get; set; }
        public string? AccountSymbolName { get; set; }
        public string? DebitSide { get; set; }
        public string? CreditSide { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public Guid Identifier { get; set; } = Guid.NewGuid();
        public int? CodeUnit { get; set; } = 100;
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
