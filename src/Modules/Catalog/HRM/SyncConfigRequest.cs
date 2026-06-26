using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM;
public class SyncConfigRequest
{
    public string Param { get; set; } = string.Empty;
    public string CodeUnit { get; set; } = string.Empty;
    public string CodeUser { get; set; } = string.Empty;
}
