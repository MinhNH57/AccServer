using System.ComponentModel.DataAnnotations;
using MiniExcelLibs.Attributes;

namespace SmartAccCloud.Application.Models.ExcelModel
{
    public class ObjectExcelDto
    {
        [ExcelColumn(Name = "Số CCCD(*)", Index = 0)]
        public string CitizenIDNumber { get; set; }
        [ExcelColumn(Name = "Ngày cấp(*)", Index = 1)]
        public DateTime? RangeDate { get; set; }

        [ExcelColumn(Name = "Nơi cấp", Index = 2)]
        public string? GrantedBy { get; set; }

        [ExcelColumn(Name = "Năm công tác", Index = 3)]
        public decimal? YearsOfWork { get; set; }

        [ExcelColumn(Name = "Giới tính", Index = 4)]
        public string? ObjSex { get; set; }

        [ExcelColumn(Name = "Ngày sinh(*)", Index = 5)]
        public DateTime? DateOfBirth { get; set; }

        [ExcelColumn(Name = "Tên đơn vị", Index = 6)]
        public string? ObjName { get; set; }

        [ExcelColumn(Name = "Mức thu nhập", Index = 7)]
        public decimal? DebitBalance { get; set; }

        [ExcelColumn(Name = "Mã sản phẩm(*)", Index = 8)]
        public string? CreditProductCode { get; set; }

        [ExcelColumn(Name = "Tên sản phẩm(*)", Index = 9)]
        public string? CreditProductName { get; set; }

        [ExcelColumn(Name = "Xã, phường", Index = 10)]
        public string? NameWards { get; set; }

        [ExcelColumn(Name = "Thôn, nhóm", Index = 11)]
        public string? GrpName { get; set; }

        [ExcelColumn(Name = "Địa chỉ", Index = 12)]
        public string? ObjAddress { get; set; }

        [ExcelColumn(Name = "Số tài khoản", Index = 13)]
        public string? AccountNumber { get; set; }

        [ExcelColumn(Name = "Ngân hàng", Index = 14)]
        public string? BankName { get; set; }

        [ExcelColumn(Name = "Email", Index = 15)]
        public string? Email { get; set; }

        [ExcelColumn(Name = "Điện thoại", Index = 16)]
        public string? PhoneNumber { get; set; }

        [ExcelColumn(Name = "Nghề nghiệp", Index = 17)]
        public string? ObjJob { get; set; }

        [ExcelColumn(Name = "Chức vụ", Index = 18)]
        public string? Position { get; set; }

        [ExcelColumn(Name = "Ghi chú", Index = 19)]
        public string? Notes { get; set; }

        [ExcelColumn(Name = "Họ tên người kế thừa(*)", Index = 20)]
        public string? MyObjName { get; set; }

        [ExcelColumn(Name = "Ngày sinh người kế thừa(*)", Index = 21)]
        public DateTime? MyDOB { get; set; }

        [ExcelColumn(Name = "Ngày cấp người kế thừa(*)", Index = 22)]
        public DateTime? MyRangeDate { get; set; }

        [ExcelColumn(Name = "Số CCCD người kế thừa(*)", Index = 23)]
        public string? MyCitizenID { get; set; }

        [ExcelColumn(Name = "Quan hệ", Index = 24)]
        public string? MyRelationship { get; set; }

        [ExcelColumn(Name = "Nơi cấp người kế thừa", Index = 25)]
        public string? MyGrantedBy { get; set; }

        [ExcelColumn(Name = "Địa chỉ người kế thừa", Index = 26)]
        public string? MyObjAddress { get; set; }

        // Các thuộc tính không có Index giữ nguyên vị trí
        public Guid Id { get; set; } = Guid.NewGuid();
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; }
        public string? GrpCode { get; set; }
        public string? CodeRoom { get; set; }
        public string? NameRoom { get; set; }
        public string? GrpAreaCode { get; set; }
        public string? GrpAreaName { get; set; }
        public string? AreaCode { get; set; }
        public string? AreaName { get; set; }
        public string? CodeManager { get; set; }
        public string? NameManager { get; set; }
        public string? ObjCode { get; set; }
        public double? DiscountRate { get; set; }
        public string? AccPosition { get; set; }
        public string? DirectorName { get; set; }
        public string? AccName { get; set; }
        public string? TaxCode { get; set; }
        public decimal? DebitLetterOfGuarantee { get; set; }
        public decimal? DebitHeadShops { get; set; }
        public decimal? DebitMortgage { get; set; }
        public int? DayNumberPayment { get; set; }
        public string? BusinessRegistrationNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsStore { get; set; }
        public string? PermanentAddress { get; set; }
        public DateTime? DOB { get; set; }
        public double? Income { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CodeOther { get; set; }
        public string? CodeUnitManager { get; set; }
        public string? NameUnitManager { get; set; }
        public string? ContractNumber { get; set; }
        public string? Buyer { get; set; }
        public string? LevelDiscount { get; set; }
        public string? DataType { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? CodeWards { get; set; }
        public string? CreatedBy { get; set; }
        public double? AccumulatedPoints { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public string? ZaloName { get; set; }
        public string? FacebookName { get; set; }
        public string? MemberRate { get; set; }
        public bool VAT { get; set; }
        public int? CitizenID { get; set; }
        public string? GuarantorNameJob { get; set; }
        public int? WorkYear { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public bool NoInvoice { get; set; }
        public bool IsUse { get; set; }
    }
}
