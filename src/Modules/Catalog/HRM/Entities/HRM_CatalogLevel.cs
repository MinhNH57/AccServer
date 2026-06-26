using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogLevel
{
    public Guid? Id { get; set; }                // PK
                                                 //public int IdAsc { get; set; }
    public string? LevelCode { get; set; }
    public string? LevelName { get; set; }      // nvarchar(100)
    public bool? Arrange { get; set; }
    public bool? Show { get; set; }
    public bool? PositionLevel { get; set; }
    public string? Notes { get; set; }          // nvarchar(500)
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
