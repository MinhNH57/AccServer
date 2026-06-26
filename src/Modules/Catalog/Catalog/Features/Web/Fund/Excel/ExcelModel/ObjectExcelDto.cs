using MiniExcelLibs.Attributes;

namespace Catalog.Features.Web.Fund.Excel.ExcelModel
{
    public class ObjectExcelDto
    {
        [ExcelColumn(Name = "Mã thành viên", Index = 0)]
        public string? ObjCode { get; set; }

        [ExcelColumn(Name = "Tên thành viên", Index = 1)]
        public string? ObjName { get; set; }

        [ExcelColumn(Name = "Số CMND-CCCD", Index = 2)]
        public string? CitizenIDNumber { get; set; }

        [ExcelColumn(Name = "Ngày cấp", Index = 3)]
        public DateTime? RangeDate { get; set; }

        [ExcelColumn(Name = "Nơi cấp", Index = 4)]
        public string? GrantedBy { get; set; }

        [ExcelColumn(Name = "Ngày sinh", Index = 5)]
        public DateTime? DateOfBirth { get; set; }

        [ExcelColumn(Name = "Giới tính", Index = 6)]
        public string? ObjSex { get; set; }

        [ExcelColumn(Name = "Năm công tác", Index = 7)]
        public int? WorkYear { get; set; }

        [ExcelColumn(Name = "Mức thu nhập", Index = 8)]
        public decimal? Income { get; set; }

        [ExcelColumn(Name = "Nghề nghiệp", Index = 9)]
        public string? GuarantorNameJob { get; set; }

        [ExcelColumn(Name = "Mã đối tượng vay vốn", Index = 10)]
        public string? DisbursementFormCode { get; set; }

        [ExcelColumn(Name = "Địa chỉ", Index = 11)]
        public string? AddressObject { get; set; }

        [ExcelColumn(Name = "Số tài khoản", Index = 12)]
        public string? AccountNumber { get; set; }

        [ExcelColumn(Name = "Tên ngân hàng", Index = 13)]
        public string? BankName { get; set; }

        [ExcelColumn(Name = "Điện thoại", Index = 14)]
        public string? PhoneNumber { get; set; }

        [ExcelColumn(Name = "Email", Index = 15)]
        public string? Email { get; set; }

        [ExcelColumn(Name = "Mã CĐ CT", Index = 16)]
        public string? CodeWards { get; set; }

        [ExcelColumn(Name = "Mã CĐ CS", Index = 17)]
        public string? GrpCode { get; set; }

        [ExcelColumn(Name = "Mã cán bộ quản lý", Index = 18)]
        public string? CodeManager { get; set; }

        [ExcelColumn(Name = "Dư nợ vay tại các tổ chức tín dụng", Index = 19)]
        public decimal? RestMoneyDebt { get; set; }

        [ExcelColumn(Name = "Số lượng Tổ chức tín dụng đang quan hệ", Index = 20)]
        public int? LoanAmount { get; set; }

        [ExcelColumn(Name = "Thời gian phát sinh dư nợ trong 12 tháng gần nhất", Index = 21)]
        public DateTime? DebitDate { get; set; }

        [ExcelColumn(Name = "Nợ xấu trong 3 năm gần nhất", Index = 22)]
        public decimal? BadDebtBalance { get; set; }

        [ExcelColumn(Name = "Nợ cần chú ý trong 12 tháng gần nhất", Index = 23)]
        public decimal? OutstandingDebtNeedsAttention { get; set; }

        [ExcelColumn(Name = "Ghi chú", Index = 24)]
        public string? Notes { get; set; }

        [ExcelColumn(Name = "Mã sản phẩm", Index = 25)]
        public string? CreditProductCode { get; set; }

        [ExcelColumn(Name = "Giá trị hợp đồng", Index = 26)]
        public decimal? ContractValue { get; set; }

        [ExcelColumn(Name = "Mã nguồn kinh phí", Index = 27)]
        public string? FundingSourceCode { get; set; }

        [ExcelColumn(Name = "Tên nguồn kinh phí", Index = 28)]
        public string? FundingSourceName { get; set; }

        [ExcelColumn(Name = "Ngày ký Hợp đồng vay vốn", Index = 29)]
        public DateTime? DisbursementDate { get; set; }

        [ExcelColumn(Name = "Mã mục đích", Index = 30)]
        public string? PurposeCode { get; set; }

        [ExcelColumn(Name = "Tạo việc làm mới (Đối với vay sản xuất kinh doanh)", Index = 31)]
        public decimal? CreateJobs { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();
        //public int? IdAsc { get; set; }
        public int? CodeUnit { get; set; }
        public string? GrpName { get; set; }
        public string? CodeRoom { get; set; }
        public string? NameRoom { get; set; }
        public string? GrpAreaCode { get; set; }
        public string? GrpAreaName { get; set; }
        public string? AreaCode { get; set; }
        public string? AreaName { get; set; }
        public string? NameManager { get; set; }
        public double? DiscountRate { get; set; }
        public string? Position { get; set; }
        public string? AccPosition { get; set; }
        public string? ObjAddress { get; set; }
        public string? DirectorName { get; set; }
        public string? AccName { get; set; }
        public string? TaxCode { get; set; }
        public decimal? DebitBalance { get; set; }
        public decimal? DebitLetterOfGuarantee { get; set; }
        public decimal? DebitHeadShops { get; set; }
        public decimal? DebitMortgage { get; set; }
        public int? DayNumberPayment { get; set; }
        public string? BusinessRegistrationNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsStore { get; set; }
        public string? PermanentAddress { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CitizenID { get; set; }
        public string? MyObjName { get; set; }
        public DateTime? MyDOB { get; set; }
        public DateTime? MyRangeDate { get; set; }
        public int? MyCitizenID { get; set; }
        public string? MyGrantedBy { get; set; }
        public string? MyObjAddress { get; set; }
        public string? MyRelationship { get; set; }
        public string? MyPhoneNumber { get; set; }
        public string? CodeOther { get; set; }
        public string? CodeUnitManager { get; set; }
        public string? NameUnitManager { get; set; }
        public string? ContractNumber { get; set; }
        public bool VAT { get; set; }
        public string? Buyer { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? AccumulatedPoints { get; set; }
        public string? LevelDiscount { get; set; }
        public string? DataType { get; set; }
        public string? NumberImport { get; set; }
        public bool IsCreated { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public string? TypeData { get; set; }
        public string? CreditProductName { get; set; }
        public string? NameWards { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public string? PurposeName { get; set; }
        public string? DisbursementFormName { get; set; }
        public bool ExistsCode { get; set; }
        public bool ExistsIDNumber { get; set; }
        public bool IsCreateSubmiss { get; set; }
        public Guid IdSubmission { get; set; } = Guid.Empty;
        public bool Proposal { get; set; }
        public bool Register { get; set; }
        public string? Submission { get; set; }
        public string? NoSubmission { get; set; }
        public int? CreditPeriod { get; set; }
        public double? InterestMonth { get; set; }
    }
}
