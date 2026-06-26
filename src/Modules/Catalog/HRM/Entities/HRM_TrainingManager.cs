using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_TrainingManager
{
    public Guid? Id { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public string? CodePosition { get; set; }
    public string? NamePosition { get; set; }
    public string? CreateBy { get; set; }
    public string? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
    public string? Type { get; set; }
    public string? ManageTrading { get; set; }
}
