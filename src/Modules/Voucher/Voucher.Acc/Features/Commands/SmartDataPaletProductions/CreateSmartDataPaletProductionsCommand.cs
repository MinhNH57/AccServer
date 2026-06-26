using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.SmartDataPaletProductions;

public class CreateSmartDataPaletProductionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("create-palet-product", CreateSmartDataPaletProductions)
            .WithTags("Vouchers");
    }

    private async Task<IResult> CreateSmartDataPaletProductions([AsParameters] VoucherServices services,
        Model.SmartDataPaletProductions request, CancellationToken token)
    {
        var command = new CreateSmartDataPaletProductionsCommand(request);
        var result = await services.Mediator.Send(command, token);
        if (result.IsSuccess)
            return TypedResults.Ok(result);
        return TypedResults.BadRequest(result);
    }
}

public record CreateSmartDataPaletProductionsCommand(Model.SmartDataPaletProductions SmartDataPaletProductions) : ICommand<Result>;

public class CreateSmartDataPaletProductionsCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser) : ICommandHandler<CreateSmartDataPaletProductionsCommand, Result>
{
    public async Task<Result> Handle(CreateSmartDataPaletProductionsCommand command, CancellationToken cancellationToken)
    {
        command.SmartDataPaletProductions.CodeUnit = currentUser.CodeUnit;
        await dbContext.SmartDataPaletProductions.AddAsync(command.SmartDataPaletProductions, cancellationToken);

        int rowCount = await dbContext.SaveChangesAsync(cancellationToken);
        if (rowCount > 0)
            return Result.Success(true);
        return Result.Failure(new Error("400", "Thêm mới thất bại, có lỗi ở đâu đó"));
    }
}