namespace SmartAccCloud.Application.Models.Catalogs.Object;

public class ObjectVm
{
    public Guid Id { get; set; }
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? GrpCode { get; set; }
    public string? GrpName { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public string? GrpAreaCode { get; set; }
    public string? GrpAreaName { get; set; }
    public string? AreaCode { get; set; }
    public string? AreaName { get; set; }
    public string? CodeManager { get; set; }
    public string? NameManager { get; set; }
    public string ObjCode { get; set; }
    public string? ObjName { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DOB { get; set; }
    public string? ObjSex { get; set; }
    public string? ObjJob { get; set; }
    public double? Income { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? CitizenID { get; set; }
    public DateTime? RangeDate { get; set; }
    public string? GrantedBy { get; set; }
    public string? MyObjName { get; set; }
    public DateTime? MyDOB { get; set; }
    public DateTime? GuaranteeExpirationDate { get; set; }
    public int? MyCitizenID { get; set; }
    public string? MyGrantedBy { get; set; }
    public string? MyObjAddress { get; set; }
    public string? MyRelationship { get; set; }
    public string? MyPhoneNumber { get; set; }
    public string? CodeOther { get; set; }
    public string? CodeUnitManager { get; set; }
    public string? NameUnitManager { get; set; }
    public string? CitizenIDNumber { get; set; }
    public string? DataType { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ShortAddress { get; set; }
    public string? ShortName { get; set; }
}