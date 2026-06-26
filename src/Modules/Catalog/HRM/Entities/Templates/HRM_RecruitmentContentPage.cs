using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Templates;
public class HRM_RecruitmentContentPage
{
    public Guid? Id { get; set; }
    public string? RecruitmentContentPageCode { get; set; }
    public string? CodeRecruitmentPageTemplate { get; set; }
    public Guid? IdContents { get; set; }
    public string? Title { get; set; }
    public string? Caption { get; set; }
    public string? CardContents { get; set; }
    public string? SubTitle1 { get; set; }
    public string? Contents { get; set; }
    public string? QuestionAndAnswerJson { get; set; }
    public string? LinkMenuJson { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
}
