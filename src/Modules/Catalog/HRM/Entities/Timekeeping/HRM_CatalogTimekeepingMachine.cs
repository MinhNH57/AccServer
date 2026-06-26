using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Timekeeping;
public class HRM_CatalogTimekeepingMachine
{
    public string? MachineCode { get; set; }
    public string? MachineName { get; set; }
    public string? MachineTCP { get; set; }
    public string? IPAddress { get; set; }
    public int? BaudRate { get; set; }
    public int? Parity { get; set; }
    public int? StopBits { get; set; }
    public bool DtrEnable { get; set; }
    public int? Handshake { get; set; }
    public int? DataBits { get; set; }
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public string? KeyWord { get; set; }
    public DateTime? DateStart { get; set; }
    public string? NameBranch { get; set; }
    public string? CodeBranch { get; set; }
    public string? GroupKeyWord { get; set; }
    public string? CodeConnect { get; set; }
    public string? NameConnect { get; set; }
    public string? Port { get; set; }
    public string? OtherInfo { get; set; }
    public string? Seris { get; set; }
    public string? CodeRegistration { get; set; }
    public string? PassWordDevice { get; set; }
}
