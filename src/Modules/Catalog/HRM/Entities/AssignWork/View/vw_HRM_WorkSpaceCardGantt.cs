using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork.View;
public class vw_HRM_WorkSpaceCardGantt
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Progress { get; set; }
    public string? Type { get; set; } = string.Empty;
    public string? IdBoard { get; set; } = string.Empty;
}
