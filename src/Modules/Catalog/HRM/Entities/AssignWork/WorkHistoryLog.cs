using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class WorkHistoryLog
{
    public Guid? Id { get; set; }
    public string? TableName { get; set; }
    public string? RecordId { get; set; }
    public string? ActionType { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? UserName { get; set; }
    public string? UserCode { get; set; }
    public string? Contents { get; set; }
    public DateTime? ActionDate { get; set; }
}
