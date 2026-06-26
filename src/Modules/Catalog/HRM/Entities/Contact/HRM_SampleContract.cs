using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Contact;
public class HRM_SampleContract
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? CodeUnit { get; set; }
    public string? CodeGrp { get; set; }
    public string? NameGrp { get; set; }
    public string? SampleContractCode { get; set; }
    public string? SampleContractName { get; set; }
    public string? CodeContentContact { get; set; }
    public string? NameContentContact { get; set; }
    public string? CodeOtherIncome { get; set; }
    public string? NameOtherIncome { get; set; }
    public double? AllowanceCoefficient { get; set; }
    public string? Contents { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsActive { get; set; }
    public double? AllowanceCoefficientOther { get; set; }
}
