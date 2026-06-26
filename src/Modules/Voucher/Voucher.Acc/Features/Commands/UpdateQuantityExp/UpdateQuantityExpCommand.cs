using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.UpdateQuantityExp;

public class UpdateQuantityExpModel
{
    public Guid IdSource { get; set; }
    public double Quantity { get; set; }
}
public record UpdateContentVocherCommand(List<UpdateQuantityExpModel> Content) : ICommand<Result>;

public class UpdateContentVocherCommandHandler(VoucherDbContext dbContext) : ICommandHandler<UpdateContentVocherCommand, Result>
{
    public async Task<Result> Handle(UpdateContentVocherCommand request, CancellationToken cancellationToken)
    {
        int rowCount = 0;
        foreach (var item in request.Content)
        {
            var rowEx = await dbContext.TradingSmartContentsData.Where(c => c.IdSource == item.IdSource)
                   .ExecuteUpdateAsync(c => c.SetProperty(a => a.QuantityExp, item.Quantity), cancellationToken: cancellationToken);
            rowCount += rowEx;
        }
        if (rowCount > 0)
        {
            return Result.Success();
        }
        return Result.Failure(new(ErrorCode.BadRequest, "Not ok"));
    }
}


public class UpdateContentVocherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapPut("/update-content", UpdateContentVocher)
            .WithTags("Vouchers");
    }

    private async Task<IResult> UpdateContentVocher([AsParameters] VoucherServices services, UpdateContentVocherCommand request)
    {
        var rsl = await services.Mediator.Send(request);

        if (rsl.IsSuccess)
            return TypedResults.Ok(rsl);
        return TypedResults.BadRequest(rsl);
    }
}