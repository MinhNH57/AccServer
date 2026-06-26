using System.ComponentModel.DataAnnotations;

namespace Catalog.SGas.Entities
{
    public class CatalogRevExp
    {
        [Key]
        [Required(ErrorMessage = "Mã phiếu không được để trống")]
        public string CodeRevExp { get; set; }
        public string? GrpCode { get; set; } = "";
        public string? GrpName { get; set; } = "";
        public string? NameRevExp { get; set; } = "";
        [Required(ErrorMessage = "Mã loại phiếu không được để trống")]
        public string TypeRevExp { get; set; }
        public string? Notes { get; set; } = "";
        public bool IsActive { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public bool ByObject { get; set; } = false; 
        public bool ByBank { get; set; }
        public bool ByTransfer { get; set; }
        public string? TypeCodeRevExp { get; set; }
        public string? TypeNameRevExp { get; set; }
    }
}
