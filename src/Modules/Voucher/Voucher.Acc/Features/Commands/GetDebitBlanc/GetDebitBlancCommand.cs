using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.GetDebitBlanc;

public class GetDebitBlancRequest
{
    public string? ObjectCode { get; set; }
    public string Date { get; set; } = DateTime.Now.ToString("MM-dd-yyyy");
    public int CodeUnit { get; set; } = 888;
    public string? AcountNumber { get; set; }
    public string? ContractCode { get; set; } 
    public string? InvoiceNumber { get; set; }

}



public record GetDebitBlancCommand(GetDebitBlancRequest Request) : ICommand<Result>;

public class GetDebitBlancCommandHandler(SmartDataServices dataServices,VoucherDbContext dbContext)
    : ICommandHandler<GetDebitBlancCommand, Result>
{
    public async Task<Result> Handle(GetDebitBlancCommand command, CancellationToken cancellationToken)
    {
        string invoiceParam = command.Request.InvoiceNumber == null? "NULL": $"'{command.Request.InvoiceNumber}'";
        string contractParam = command.Request.ContractCode == null ? "NULL": $"'{command.Request.ContractCode}'";
        var result = await dataServices.GetListObject<object>($"select * from [dbo].[GetDebtBalanceByVoucher]('{command.Request.ObjectCode ?? ""}','{command.Request.Date}',{command.Request.CodeUnit},'{command.Request.AcountNumber}',{contractParam},{invoiceParam})", dbContext.Database.GetConnectionString());

        return Result.Success(result.ToList());
    }
}