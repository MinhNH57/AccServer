using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_InterviewQuestions
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? InterviewQuestionCode { get; set; }
    public string? InterviewQuestionName { get; set; }
    public string? QuestionList { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreateDate { get; set; } = DateTime.Now;
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public string? CreateBy { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
