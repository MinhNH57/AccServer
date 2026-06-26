namespace SmartAccCloud.Application.Services.Extension;

public class GetNoCouponRequest
{
    public bool IsDate { get; set; } = true;
    public DateTime Date { get; set; } = DateTime.Today.Date;
    public string TableName { get; set; } = "SmartData";
    public string DataType { get; set; } = string.Empty;
}