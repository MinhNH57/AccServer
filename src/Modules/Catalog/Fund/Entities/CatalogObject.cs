using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Entities;

public class CatalogObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
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
    [Key]
    [Required(ErrorMessage = "Mã đơn vị không được để trống")]
    public string ObjCode { get; set; }
    [Required(ErrorMessage = "Không được để trống tên đơn vị")]
    public string? ObjName { get; set; }
    public double? DiscountRate { get; set; }
    public string? Position { get; set; }
    public string? AccPosition { get; set; }
    public string? ObjAddress { get; set; }
    public string? DirectorName { get; set; }
    public string? AccName { get; set; }
    public string? TaxCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? AccountNumber { get; set; }
    public string? BankName { get; set; }
    public decimal? DebitBalance { get; set; }
    [Precision(16, 2)]
    public decimal? DebitLetterOfGuarantee { get; set; }
    [Precision(16, 2)]
    public decimal? DebitHeadShops { get; set; }
    [Precision(16, 2)]
    public decimal? DebitMortgage { get; set; }
    public int? DayNumberPayment { get; set; }
    public string? BusinessRegistrationNumber { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public bool IsStore { get; set; }
    //public bool IsInternal { get; set; }
    public string? PermanentAddress { get; set; }
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
    public DateTime? MyRangeDate { get; set; }
    public string? MyCitizenID { get; set; }
    public string? MyGrantedBy { get; set; }
    public string? MyObjAddress { get; set; }
    public string? MyRelationship { get; set; }
    public string? MyPhoneNumber { get; set; }
    public string? CodeOther { get; set; }
    public string? CodeUnitManager { get; set; }
    public string? NameUnitManager { get; set; }
    public string? ContractNumber { get; set; }
    public string? Buyer { get; set; }
    public string? LevelDiscount { get; set; }
    public string? CitizenIDNumber { get; set; }
    public string? DataType { get; set; }
    public string? CreatedBy { get; set; }
    public double? AccumulatedPoints { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ZaloName { get; set; }
    public string? FacebookName { get; set; }
    public string? MemberRate { get; set; }
    public string? ShortAddress { get; set; }
    public string? ShortName { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool VAT { get; set; }
    public bool NoInvoice { get; set; }
    public DateTime? CreateDate { get; set; } = DateTime.Now;
    public string? ObjSource { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }

    //public DateTime? DateEnd { get; set; }

    //public string? CodeWards { get; set; }
    //public string? NameWards { get; set; }

    //public DateTime? DateOfIssue { get; set; }

    //public string? CodeHamlet { get; set; }
    //public string? NameHamlet { get; set; }
    //public string? ObjCodeOld { get; set; }
    //[Precision(16, 2)]
    //public decimal? YearsOfWork { get; set; }
    //public string? GuarantorNameJob { get; set; }
    //public int? WorkYear { get; set; }

    //public string? CreateBy { get; set; }
    //public DateTime? ModifyDate { get; set; }
    //public string? ModifyBy { get; set; }

    //public bool IsUse { get; set; }

    public string? AccountName { get; set; }
    //public string? CreditProductCode { get; set; }
    //public string? CreditProductName { get; set; }


    //public bool IsStaff { get; set; } = false;
    //public bool IsSupplier { get; set; } = false;
    //public bool IsCustomer { get; set; } = false;
    //public bool IsBank { get; set; }
    //public bool IsOther { get; set; }
    //public string? PaymentTerms { get; set; }
    //public int? DaysOwed { get; set; }
    //[Precision(18,0)]
    //public decimal? MaximumDebtAmount { get; set; }
    //public string? AccountsPayable { get; set; }
    //public string? CustomField1 { get; set; }
    //public string? CustomField2 { get; set; }
    //public string? CustomField3 { get; set; }
    //public string? CustomField4 { get; set; }
    //public string? CustomField5 { get; set; }
}