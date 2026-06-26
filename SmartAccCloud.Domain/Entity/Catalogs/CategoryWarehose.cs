using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartAccCloud.Domain.Validation;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CategoryWarehose
    {
        [Key]
        [Unique(nameof(CategoryWarehose), nameof(CodeWarehose), ErrorMessage = "Giá trị này đã tồn tại")]
        [Required(ErrorMessage = "Mã kho không được để trống")]
        public string CodeWarehose { get; set; }
        public string? NameWarehose { get; set; }
        public string? AddressWarehose { get; set; }
        public string? CodeStocker { get; set; }
        public string? NameStocker { get; set; }
        public bool? CostPrice { get; set; }
        public bool IsStore { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public int IdAsc { get; set; }
        public int CodeUnit { get; set; }
        public int? IdInv { get; set; }
        public string? CodeInv { get; set; }
        public string? NameInv { get; set; }
        public string? Branch { get; set; }
        public string? InvoiceTemplate { get; set; }
        public string? InvoiceSymbol { get; set; }
        public bool TaxSeparation { get; set; }
        public bool NotBog { get; set; }
        public bool NotEnviroment { get; set; }
        public bool AutoPublish { get; set; }
    }
}
