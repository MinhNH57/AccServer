using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.HRM_Catalog;
public class HRM_CatalogAssetGroup
{
    public string? AssetGroupCode { get; set; }
    public string? AssetGroupName { get; set; }
    public string? Notes { get; set; }
    public bool? IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public double? PercentPerAllocationPeriod { get; set; }
    public int? NumberYear { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
}
