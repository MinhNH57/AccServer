using System;

namespace RefNo.API.Model;

public class NumberingRule : IBaseEntity
{
    public Guid? TenantId { get; set; }
    public Guid AutoId { get; set; }
    public Guid? BranchId { get; set; }
    public int? RefTypeCategory { get; set; }
    public int? LengthOfValue { get; set; }
    public int? DisplayOnBook { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public double? Value { get; set; }
    public string RefTypeCategoryName { get; set; }
    public string RefTypeCategoryNameEnglish { get; set; }
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }

    public Guid? DatabaseId { get; set; }
    public double? MaxValue { get; set; }
    public int? StateMode { get; set; }
}
