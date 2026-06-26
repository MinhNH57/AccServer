using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities
{
    public class CatalogMethodOfPayments
    {
        [Key]
        
        [Required(ErrorMessage = "Mã phương thức không được để trống")]
        public string MethodOfPaymentsCode { get; set; }
        public string? MethodOfPaymentsName { get; set; }
        public string? ObjectCode { get; set; }
        public string? ObjectName { get; set; }
        public string? TypePayments { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
    }
}
