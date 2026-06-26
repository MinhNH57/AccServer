using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.Extensions.Configuration;
using Refit;
using Voucher.Acc.ExternalApis;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Fund;

public class CreatFundSmartMoneyPaidModel
{
    public Guid IdVouchers { get; set; }
    public string? FundingSourceCode { get; set; }
    public decimal? Amount { get; set; }
    public string? Notes { get; set; }
    public string TokenMessage { get; set; }
    public string? GroupName { get; set; }
    public string? NumberOfVouchers { get; set; }
}

public record CreateFundSmartMoneyPaidCommand(CreatFundSmartMoneyPaidModel FundSmartMoneyPaid) : ICommand<Result>;


public class CreateFundSmartMoneyPaidCommandHandlder(VoucherDbContext dbContext, ICurrentUser currentUser, IConfiguration configuration) : ICommandHandler<CreateFundSmartMoneyPaidCommand, Result>
{
    public async Task<Result> Handle(CreateFundSmartMoneyPaidCommand request, CancellationToken cancellationToken)
    {
        var entityCreate = new FundSmartMoneyPaid()
        {
            CodeUnit = currentUser.CodeUnit,
            RecordDate = DateTime.Now,
            IdVouchers = request.FundSmartMoneyPaid.IdVouchers,
            FundingSourceCode = request.FundSmartMoneyPaid.FundingSourceCode,
            Amount = request.FundSmartMoneyPaid.Amount,
            Notes = request.FundSmartMoneyPaid.Notes
        };

        await dbContext.FundSmartMoneyPaid.AddAsync(entityCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var nofiServiceUrl = configuration.GetValue<string>("NotificationServiceUrl");
        var notiApi = RestService.For<INotificationService>(nofiServiceUrl);
        var notiRequest = new NotificationRequest()
        {
            Title = "HA NOI FEI",
            Body = $"CĐCS: {request.FundSmartMoneyPaid.GroupName}. " +
                   $"\nĐã nộp về chương trình {request.FundSmartMoneyPaid.FundingSourceCode} " +
                   $"\nSố phiếu: {request.FundSmartMoneyPaid.NumberOfVouchers}" +
                   $"\nSố tiền: {request.FundSmartMoneyPaid.Amount:N0} VNĐ",
            Tokens = [request.FundSmartMoneyPaid.TokenMessage]
        };
        await notiApi.PushNotification(notiRequest);
        return Result.Success("Create data successfully");
    }
}