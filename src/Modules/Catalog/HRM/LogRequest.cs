using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM;
public class LogRequest
{
    public string? TableName { get; set; }
    public string? RecordId { get;set; } = Guid.NewGuid().ToString();
    public string? ActionType { get;set; }
    public string? OldValue { get;set; }
    public string? NewValue { get;set; }
    public string? UserCode { get;set; }
    public string? Contents { get;set; }
}
