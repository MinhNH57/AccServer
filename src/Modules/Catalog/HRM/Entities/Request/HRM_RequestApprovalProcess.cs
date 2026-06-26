using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Request;
public class HRM_RequestApprovalProcess
{
    public Guid? Id { get; set; }
    public string? IdContents { get;set; }
    public string? Title { get; set; }
    public string? LstJsonApproval { get; set; }
    public string? CodeUnit { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
}
