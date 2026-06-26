using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogFormOfWork
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string? FormWorkCode { get; set; }
    public string? FormWorkName { get; set; }
    public string? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreateDate { get; set; } = DateTime.Now;
    public string? CreateBy { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
