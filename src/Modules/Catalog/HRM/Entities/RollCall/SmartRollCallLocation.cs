using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.RollCall;
public class SmartRollCallPhone
{
    public Guid? Id { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int CodeUnit { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? LocationCode { get; set; }
    public bool? IsActive { get; set; }
    //
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? BssId { get; set; }
    public string? CodeMainBranch { get; set; }
    public string? NameMainBranch { get; set; }
    public string? SubBranch { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public bool? BasedOnWifiName { get; set; }
    public bool? BaseOnWifiNameAndBssid { get; set; }
    public bool? BaseOnImage { get; set; }
    public string? OtherInfo { get; set; }
    public bool? ShareLocation { get; set; }
    public string? QRImage { get; set; }
    public string? WanIP { get; set; }
    public string? Address { get; set; }
    public string? CodeProject { get; set; }
    public string? NameProject { get; set; }
    public double? DisplayAllow { get;set; }
}
