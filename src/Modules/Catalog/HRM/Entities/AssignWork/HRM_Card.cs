using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class HRM_Card
{
    public Guid? Id { get; set; }
    public Guid? IdContents { get; set; }
    public string? Title { get; set; }
    public string? Notes { get; set; }
    public string? ParticipantJson { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsDone { get; set; }
    public string? Status { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsApproved { get; set; }
    public string? ApprovedBy
    {
        get; set;
    }
}
