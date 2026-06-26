namespace Report.Infrastructure.StoreModels;

public class ReportFundSavingsSummaryBook
{
    public int Id { get; set; } // Mã định danh bản ghi

    public int? NumericalOrder { get; set; } // Thứ tự số
    public int? MaxNumericalOrder { get; set; } // Thứ tự số tối đa
    public string? SmartSort { get; set; } // Sắp xếp thông minh
    public string? UserCode { get; set; } // Mã người dùng
    public string? Parameter { get; set; } // Tham số
    public int? CodeUnit { get; set; } // Mã đơn vị
    public string? NameUnit { get; set; } // Tên đơn vị
    public string? DataType { get; set; } // Loại dữ liệu
    public Guid? IdVoucher { get; set; } // Mã chứng từ
    public DateTime? RecordDate { get; set; } // Ngày ghi nhận
    public DateTime? VoucherDate { get; set; } // Ngày chứng từ
    public bool? NoExcel { get; set; } // Không xuất Excel
    public bool Obligatory { get; set; } = false; // Bắt buộc (mặc định là 0)

    public string? ContractNumber { get; set; } // Số hợp đồng
    public string? ContractNumberOld { get; set; } // Số hợp đồng cũ
    public DateTime? SignDate { get; set; } // Ngày ký hợp đồng
    public DateTime? DateDue { get; set; } // Ngày đến hạn
    public DateTime? DateOfBirth { get; set; } // Ngày sinh

    public string? DayBefore { get; set; } // Ngày trước
    public string? EndDate { get; set; } // Ngày kết thúc
    public string? BeginDate { get; set; } // Ngày bắt đầu
    public string? Date { get; set; } // Ngày chung
    public string? NumberOfVouchers { get; set; } // Số lượng chứng từ
    public string? Description { get; set; } // Mô tả

    public string? ObjectCode { get; set; } // Mã đối tượng
    public string? ObjectName { get; set; } // Tên đối tượng
    public string? CitizenIDNumber { get; set; } // Số CMND
    public string? HamletCode { get; set; } // Mã tổ
    public string? HamletName { get; set; } // Tên tổ
    public string? GroupCode { get; set; } // Mã cụm
    public string? GroupName { get; set; } // Tên cụm
    public string? CodeWards { get; set; } // Mã phường xã
    public string? NameWards { get; set; } // Tên phường xã
    public string? CodeDistrict { get; set; } // Mã quận huyện
    public string? NameDistrict { get; set; } // Tên quận huyện

    public string? FundingSourceCode { get; set; } // Mã kinh phí
    public string? FundingSourceName { get; set; } // Tên kinh phí
    public string? CreditProductCode { get; set; } // Mã sản phẩm tín dụng
    public string? CreditProductName { get; set; } // Tên sản phẩm tín dụng
    public string? ContractTypeCode { get; set; } // Mã loại hợp đồng
    public string? ContractTypeName { get; set; } // Tên loại hợp đồng
    public string? PurposeCode { get; set; } // Mã mục đích
    public string? PurposeName { get; set; } // Tên mục đích

    public double? InterestRateYear { get; set; } // Lãi suất năm
    public decimal? ContractValue { get; set; } // Giá trị hợp đồng
    public int? BorrowingTime { get; set; } // Thời gian vay
    public decimal? PrincipalBegin { get; set; } // Dư gốc đầu kỳ
    public decimal? InterestBegin { get; set; } // Dư lãi đầu kỳ
    public decimal? TotalBegin { get; set; } // Tổng dư gốc, lãi đầu kỳ
    public decimal? MembersBegin { get; set; } // Số thành viên đầu kỳ
    public decimal? MembersLoans { get; set; } // Số thành viên vay vốn

    public decimal? NumberHamlet { get; set; } // Số tổ đầu kỳ
    public decimal? NumberWards { get; set; } // Số xã đầu kỳ

    public decimal? PrincipalIncrease { get; set; } // Dư gốc PS tăng
    public decimal? PrincipalIncrease6 { get; set; } // Dư gốc PS tăng sau 6 tháng
    public decimal? PrincipalIncrease12 { get; set; } // Dư gốc PS tăng sau 12 tháng
    public decimal? PrincipalIncrease24 { get; set; } // Dư gốc PS tăng sau 24 tháng
    public decimal? PrincipalIncrease36 { get; set; } // Dư gốc PS tăng sau 36 tháng

    public decimal? InterestIncrease { get; set; } // Dư lãi PS tăng
    public decimal? TotalIncrease { get; set; } // Tổng dư gốc, lãi PS tăng
    public decimal? MembersIncrease { get; set; } // Số thành viên PS tăng
    public decimal? MembersIncrease6 { get; set; } // Số thành viên PS tăng sau 6 tháng
    public decimal? MembersIncrease12 { get; set; } // Số thành viên PS tăng sau 12 tháng
    public decimal? MembersIncrease24 { get; set; } // Số thành viên PS tăng sau 24 tháng
    public decimal? MembersIncrease36 { get; set; } // Số thành viên PS tăng sau 36 tháng
    public decimal? MembersNew { get; set; } // Số thành viên mới PS
    public decimal? MembersWithdrawal { get; set; } // Số thành viên rút TK

    public decimal? Poverty { get; set; } // Số thành viên hộ nghèo
    public decimal? NearPoverty { get; set; } // Số thành viên hộ cận nghèo
    public decimal? Average { get; set; } // Số thành viên trung bình
    public decimal? Pretty { get; set; } // Số thành viên khá

    public decimal? PrincipalDecrease { get; set; } // Dư gốc PS giảm
    public decimal? InterestDecrease { get; set; } // Dư lãi PS giảm
    public decimal? TotalDecrease { get; set; } // Tổng dư gốc, lãi PS giảm
    public decimal? AmountNotPlanned { get; set; } // Lãi thoái thu
    public decimal? MembersDecrease { get; set; } // Số thành viên PS giảm

    public decimal? PrincipalEnd { get; set; } // Dư gốc cuối kỳ
    public decimal? InterestEnd { get; set; } // Dư lãi cuối kỳ
    public decimal? TotalEnd { get; set; } // Tổng dư gốc, lãi cuối kỳ
    public decimal? MembersEnd { get; set; } // Số thành viên cuối kỳ
    public decimal? NumberEnd { get; set; } // Số tổ còn lại

    public decimal? OverdueIII { get; set; } // Quá hạn nhóm >= 3
    public decimal? OverduePriorIII { get; set; } // Quá hạn kỳ trước >= 3
    public decimal? NumberOverdueIII { get; set; } // Số thành viên quá hạn nhóm III

    public decimal? Overdue { get; set; } // Quá hạn
    public decimal? OverduePrior { get; set; } // Quá hạn kỳ trước
    public decimal? NumberOverdue { get; set; } // Số thành viên quá hạn

    public decimal? CommissionsCollaborator { get; set; } // Hoa hồng cộng tác viên (2%)
    public decimal? CommissionsProvince { get; set; } // Hoa hồng tỉnh (10%)
    public decimal? CommissionsWards { get; set; } // Hoa hồng phường xã (3%)
    public decimal? AmountPaySavings { get; set; } // Số tiền thanh toán tiết kiệm

    public string? AmountInWords { get; set; } // Số tiền viết bằng chữ
    public string? Headline { get; set; } // Tiêu đề
    public string? Template { get; set; } // Tài khoản đối ứng
    public string? Decision { get; set; } // Quyết định
    public string? Time { get; set; } // Thời gian
}
