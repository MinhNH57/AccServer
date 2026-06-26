namespace Voucher.Acc.Model;

public class TravelExpenses
{
    public Guid Id {get;set;}
    //public int IsAsc {get;set;}
    public DateTime? WorkingDate {get;set;}
    public string? WorkingPlace {get;set;}
    public int? WorkingDay {get;set;}
    public decimal WorkingMoney {get;set;}
    public decimal RestMoney {get;set;}
    public decimal GasMoney {get;set;}
    public decimal? OtherMoney {get;set;}
    public decimal RoadMoney {get;set;}
    public decimal OtherMoneyWithInv {get;set;}
    public decimal TotalMoney {get;set;}
    public string? Notes {get;set;}
    public string? Description {get;set;}
    public Guid? IdContents {get;set;}
}