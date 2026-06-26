namespace Report.Models;

public class ReportFundSmartLatePayment
{
    public int Id { get; set; }
    public int? NumericalOrder { get; set; }
    public int? MaxNumericalOrder { get; set; }
    //public string SmartSort { get; set; }
    //public string UserCode { get; set; }
    //public string Parameter { get; set; }
    //public int? CodeUnit { get; set; }
    //public string NameUnit { get; set; }
    //public string DataType { get; set; }
    public Guid? IdVoucher { get; set; } // Id Hợp đồng tín dụng
    //public DateTime? RecordDate { get; set; }
    //public DateTime? VoucherDate { get; set; }
    //public bool? NoExcel { get; set; }
    //public bool Obligatory { get; set; } = false;  // default(0)
    //public string ContractNumber { get; set; }
    //public string ContractNumberOld { get; set; }
    //public DateTime? SignDate { get; set; }
    //public DateTime? DateDue { get; set; }
    //public DateTime? DateOfBirth { get; set; }
    //public string DayBefore { get; set; }
    //public string EndDate { get; set; }
    //public string BeginDate { get; set; }
    //public string Date { get; set; }
    //public string NumberOfVouchers { get; set; }
    //public string Description { get; set; }
    public string CodeManager { get; set; } // Mã quản lý
    public string NameManager { get; set; } // Tên quản lý
    public string GroupCode { get; set; } // Mã cụm
    public string GroupName { get; set; } // Tên cụm
    public string CodeWards { get; set; } // Mã Phường xã
    public string NameWards { get; set; } // Tên Phường xã
    //public decimal? BalancePrincipal { get; set; } // Dư gốc
    //public decimal? BalanceInterest { get; set; } = 0; // Dư lãi
    //public decimal? BalancePrincipalInterest { get; set; } = 0; // Tổng dư gốc, lãi quá hạn
    public decimal? ContractValue { get; set; } = 0;
    public string ObjectCode { get; set; }
    public string ObjectName { get; set; }
    public string CitizenIDNumber { get; set; }
    public string HamletCode { get; set; } // Mã tổ
    public string HamletName { get; set; } // Mã tổ
    public string? FundingSourceName { get; set; } // Tên kinh phí
    public string? FundingSourceCode { get; set; } // Mã KP
    //public string CreditProductCode { get; set; } // Mã sản phẩm

    //public string CodeDistrict { get; set; } // Mã Quận huyện
    //public string NameDistrict { get; set; } // Tên Quận huyện
    //public string CreditProductName { get; set; } // Tên sản phẩm
    //public string ContractTypeCode { get; set; } // Loại hình thức GN
    //public string ContractTypeName { get; set; } // Loại hình thức GN

    //public string PurposeCode { get; set; } // Mục đích
    //public string PurposeName { get; set; } // Tên mục đích
    //public decimal? ContractValue { get; set; } // Giá trị hợp đồng
    //public int? BorrowingTime { get; set; } // Thời gian vay
    public decimal? BalanceSavings { get; set; } // Dư tiết kiệm

    public float? PercentDue { get; set; } //Kỳ trả nợ
    public decimal? BalancePrincipalDue { get; set; } = 0; // Dư gốc quá hạn
    public decimal? BalanceInterestDue { get; set; } = 0; // Dư lãi quá hạn
    public decimal? BalancePrincipalInterestDue { get; set; } = 0; // Tổng dư gốc, lãi quá hạn
    //public decimal? PrincipalI { get; set; } = 0; // Dư gốc nhóm I
    //public decimal? InterestI { get; set; } = 0; // Dư lãi Nhóm I
    //public decimal? PrincipalII { get; set; } = 0; // Dư gốc nhóm II
    //public decimal? InterestII { get; set; } = 0; // Dư lãi Nhóm II
    //public decimal? PrincipalIII { get; set; } = 0; // Dư gốc nhóm III
    //public decimal? InterestIII { get; set; } = 0; // Dư lãi Nhóm III
    //public decimal? PrincipalIV { get; set; } = 0; // Dư gốc nhóm IV
    //public decimal? InterestIV { get; set; } = 0; // Dư lãi Nhóm IV
    //public decimal? PrincipalV { get; set; } = 0; // Dư gốc nhóm V
    //public decimal? InterestV { get; set; } = 0; // Dư lãi Nhóm V
    //public decimal? PrincipalOver { get; set; } = 0; // Dư gốc ngoài bảng
    //public decimal? InterestOver { get; set; } = 0; // Dư lãi ngoài bảng
    public decimal? RiskyDebtII { get; set; } = 0; // CĐCS
    public decimal? RiskyDebtIII { get; set; } = 0; // CĐCT
    public decimal? RiskyDebtIV { get; set; } = 0; // CTV
    //public decimal? RiskyDebtV { get; set; } = 0; // Nợ có rủi ro Nhóm V
    public decimal? RiskyDebtOver { get; set; } = 0; // TKBB hoàn trả
    //public decimal? InterestPenalty { get; set; } = 0; // Lũy kế lãi phạt chưa thu
    //public decimal? InterestPenaltyCollected { get; set; } = 0; // Lũy kế lãi phạt đã thu
    public decimal? TotalPrincipalCollected { get; set; } = 0; // Tổng thu gốc
    //public decimal? TotalInterestCollected { get; set; } = 0; // Tổng thu lãi
    //public string AmountInWords { get; set; }
    //public string Headline { get; set; }
    //public string Template { get; set; } // Tài khoản đối ứng
    //public string Decision { get; set; }
    //public string Time { get; set; }
}