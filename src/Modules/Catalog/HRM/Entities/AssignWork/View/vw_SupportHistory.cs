using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork.View;
public class vw_SupportHistory
{
    public Guid? Id { get; set; }    
    public Guid? WorkSpaceId { get; set; }
    public string? WorkSpaceName { get; set; }
    public string? WorkSpaceCode { get; set; }

    public Guid? CardId { get; set; }
    public string? CardTitle { get; set; }

    public Guid? CardItemId { get; set; }
    public string? CardItemTitle { get; set; }
    public string? CardItemCode { get; set; }

    public string? CodeCustomObj { get; set; }
    public string? NameCustomObj { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }

    public string? CodeUnit { get; set; }

    public bool? CardItemIsDone { get; set; }
    public DateTime? CardItemCreateDate { get; set; }
}
