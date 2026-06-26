using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Contact;
public class HRM_CatalogGroupContract
{
    public string? GrpCode { get; set; }
    public string? GrpName { get; set; }
    public string? Notes { get; set; }
    public Guid Id { get; set; }
    //public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
