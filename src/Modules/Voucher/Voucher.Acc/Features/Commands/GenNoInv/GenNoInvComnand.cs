using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Commands.GenNoInv;

public class GenNoInvReponse
{
    public string SmartCode { get; set; }

    //public SmartCodes(string value)
    //{
    //    Value = value;
    //}
}

public class GenNoInvRequest
{
    public string? UserCode { get; set; }
    public int? CodeUnit { get; set; } = 888;
    public bool? IsDate { get; set; } = true;
    public string? Date { get; set; } = DateTime.Now.ToString("MM-dd-yyyy");
    public string? TableName { get; set; } = "SalesSmartData";
    public string? DataType { get; set; }
}

public record GenNoInvComnand (GenNoInvRequest Request) : ICommand<Result<GenNoInvReponse>>;