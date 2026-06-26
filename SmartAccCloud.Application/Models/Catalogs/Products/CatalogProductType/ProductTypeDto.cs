using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.Products.CatalogProductType;

public class ProductTypeDto
{
    [Key]
    [Required(ErrorMessage = "Mã loại hàng hóa không được để trống")]
    public string ProductTypeCode { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    public string? ProductTypeName { get; set; }

    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    public bool NoAccumulationPoint { get; set; }
    public string? SignType { get; set; }
}