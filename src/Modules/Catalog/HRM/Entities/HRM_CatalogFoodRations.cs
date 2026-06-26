using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogFoodRations
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    //public int? IdAsc { get; set; }
    public string? FoodRationsCode { get; set; }
    public string? FoodRationsName { get; set; }
    public string? Notes { get; set; }
    public string? Codeunit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModufyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool? IsActive { get; set; }
}
