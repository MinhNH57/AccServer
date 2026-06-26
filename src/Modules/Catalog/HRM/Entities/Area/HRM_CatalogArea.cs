using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Area;
public class HRM_CatalogArea
{
    public string? GrpCodeArea { get; set; }
    public string? GrpNameArea { get; set; }
    [Key]
    [Required(ErrorMessage = "Mã khu vực không được để trống")]
    public string CodeArea { get; set; } = null!;
    public string? NameArea { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}
