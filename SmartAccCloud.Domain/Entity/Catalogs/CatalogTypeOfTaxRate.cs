using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogTypeOfTaxRate
    {
        [Key]
        public string? TypeCode { get; set; }
        public string? TypeName { get; set; }
        public double? VatRate { get; set; }
    }
}
