using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class CatalogJobPosition
{
    public Guid Id { get; set; } = new Guid();
    public string? CodeUnit { get; set; } = "888";

    //[Key]

    //[Required(ErrorMessage = "Mã vị trí không được để trống")]
    public string? CodeJobPosition { get; set; }

    //[Required(ErrorMessage = "Tên vị trí không được để trống")]
    public string? NameJobPosition { get; set; }

    public string? Notes { get; set; }
    public bool IsActive { get; set; } = false;
    public float? ToxicCompensationLevel { get; set; }
}
