using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork.View;
public class vw_HRM_WorkDailyWithContents
{
    public Guid? Id { get; set; }  
    public string WorkCode { get; set; } = null!;
    public string WorkName { get; set; } = null!; // A.WorkName
    public string CodeWorkSapce { get; set; } = null!; // A.CodeWorkSapce
    public string NameWorkSapce { get; set; } = null!; // A.NameWorkSapce
    public DateTime? CompleteAt { get; set; }       // B.CompleteAt
    public DateTime CreateDate { get; set; }        // A.CreateDate
    public string EmployeeCode { get; set; } = null!; // A.CreateBy
    public string WorkContents { get; set; } = null!; // B.WorkContents
    public string CodeUnit { get; set; } = null!;     // A.CodeUnit
    public string? IdContentDaily { get; set; }    // B.Id
    public bool? IsDone { get; set; }         // A.IsDone
    public DateTime? ModifyDate { get; set; } 
}
