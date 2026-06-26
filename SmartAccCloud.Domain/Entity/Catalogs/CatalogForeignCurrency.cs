using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogForeignCurrency
    {
        [Unique(nameof(CatalogForeignCurrency), nameof(ForeignCurrencyType), ErrorMessage = "Mã ngoại tệ đã tồn tại")]

        public string? ForeignCurrencyType { get; set; }
        public double? ExchangeRate { get; set; }
        public bool IsActive { get; set; }
        [Key]
        public Guid Id { get; set; }
        public int? CodeUnit { get; set; } = 100;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public string? Notes { get; set; }
    }
}
