using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogLocationWarehouse
{
    [Key]
    [Required(ErrorMessage = "Mã vị trí không được để trống")]
    public string LocationCode { get; set; }
    public string? LocationContent { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
