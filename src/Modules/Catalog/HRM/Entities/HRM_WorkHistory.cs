using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_WorkHistory
{
    public Guid? Id { get; set; }                  // GUID tự sinh
    //public int? IdAsc { get; set; }
    public string? WorkHistoryCode { get; set; }   // varchar(20)
    public string? WorkHistoryName { get; set; }   // nvarchar(255)
    public string? CodeObj { get; set; }           // varchar(20)
    public string? CodeWorkInformation { get; set; } // varchar(20)
    public string? WorkPlace { get; set; }         // nvarchar(255)
    public string? CodePosition { get; set; }      // varchar(20)
    public string? NamePosition { get; set; }      // nvarchar(255)
    public double? WorkingTime { get; set; }      // float (nullable)
    public DateTime? BeginDate { get; set; }      // date (nullable)
    public DateTime? EndDate { get; set; }        // date (nullable)
    public decimal? SalaryLevel { get; set; }
    public decimal? Salary { get; set; }
    public decimal?Allowances { get; set; }
    public bool? IsActive { get; set; }            // bit
    public string? CreateBy { get; set; }          // varchar(20)
    public DateTime? CreateDate { get; set; }     // datetime
    public string? ModifyBy { get; set; }          // varchar(20)
    public DateTime? ModifyDate { get; set; }
}
