using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Request;
public class HRM_Request
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? RequestCode { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsAsctive { get; set; }
    public string? Type { get; set; }
    public bool? IsObligatory { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? MoodifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
