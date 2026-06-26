using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class HRM_Comments
{
    public Guid Id { get; set; }
    //public int IdAsc { get; set; }
    public Guid CardId { get; set; }
    public string? CodeAuthor { get; set; }
    public string? NameAuthor { get; set; }
    public string? Contents { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; } = DateTime.Now;
}
