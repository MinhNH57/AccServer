using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.CameraAI;
public class HRM_CatalogCameraAI
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? CameraAICode { get; set; }
    public string? CodeCameraSeries { get; set; }
    public string? NameCameraSeries { get; set; }
    public string? ReminiscentName { get; set; }
    public string? CodeBranch { get; set; }
    public string? NameBranch { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public string? CodePosition { get; set; }
    public string? NamePosition { get; set; }
    public string? TypeRecord { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool? IsActive { get; set; }
}
