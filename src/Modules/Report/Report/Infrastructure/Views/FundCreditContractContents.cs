using System.ComponentModel.DataAnnotations;

namespace Report.Infrastructure.Views;

public class FundCreditContractContents
{
    [Key]
    public int IdAsc { get; set; }
    public Guid IdContents { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public decimal? AmountRemain { get; set; }
    //Ngày ghi sổ
    public DateTime? RecordDate { get; set; }
    //Ngày tính lãi phải trả
    public DateTime? VoucherDate { get; set; }
}