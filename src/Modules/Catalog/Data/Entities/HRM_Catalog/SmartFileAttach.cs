using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.HRM_Catalog;
public class SmartFileAttach
{
    public Guid? Id { get; set; }
    public Guid? IdContents { get; set; }
    //public int IdAsc { get; set; }
    public string? Xem { get; set; }
    public string? Description { get; set; }
    public string? PathFile { get; set; }
    public string? FileNames { get; set; }
    public string? SizeFile { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
    public string? Notes { get; set; }
    public string? NumberOfVouchers { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? TypeFile { get; set; }
    public string? CodeUser { get; set; }
}
