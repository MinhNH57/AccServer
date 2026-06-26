using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities
{
    public class CatalogForeignCurrency
    {

        public string? ForeignCurrencyType { get; set; }
        public double? ExchangeRate { get; set; }
        public bool IsActive { get; set; }
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? CodeUnit { get; set; } = 100;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public string? Notes { get; set; }
    }
}
