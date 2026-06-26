using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_RecruimentCampaign
{
    public Guid? Id { get; set; }
    //public int IdAsc {get;set;}
	public string? RecruimentCampaignCode { get; set; }
    public string? RecruimentCampaignName { get; set; }
    public string? Email { get; set; }
    public string? CodePosition { get; set; }
    public string? NamePosition { get; set; }
    public string? CodeSource { get; set; }
    public string? NameSource { get; set; }
    public decimal? Expense { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int? NumberOfClicks { get; set; }
    public int? CVSubmitted { get; set; }
    public int? Recruited { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? CreateBy { get; set; }
    public string? ModifyBy { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
