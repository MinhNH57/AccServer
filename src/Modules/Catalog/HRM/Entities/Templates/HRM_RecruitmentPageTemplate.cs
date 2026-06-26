using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Templates;
public class HRM_RecruitmentPageTemplate
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? RecruitmentPageTemplateCode { get; set; }
    public string? RecruitmentPageTemplateName { get; set; }
    public string? Header { get; set; }
    public string? Content { get; set; }
    public string? CardContent { get; set; }
    public string? Footer { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
