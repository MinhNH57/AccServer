using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class FileAttach
{
    public Guid? Id { get; set; }
    public string? ViewFile { get; set; }
    public string? DataType { get; set; }
    public string? NumberOfVoucher { get; set; }
    public string? FileNames { get; set; }
    public string? FilePath { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public string? TableName { get; set; }
    public string? KeyTable { get; set; }
    public string? Notes { get; set; }
    public string? CodeUser { get; set; }
}
