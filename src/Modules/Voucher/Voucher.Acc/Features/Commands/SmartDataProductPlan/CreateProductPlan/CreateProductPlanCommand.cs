using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model.ProductPlant;

namespace Voucher.Acc.Features.Commands.SmartDataProductPlan.CreateProductPlan;

public class CreateProductPlanApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();
        api.MapPost("create-production-plan/", CreateVoucherEndpoint)
            .WithSummary("Tạo mới kế hoạch sx");
    }

    private static async Task<IResult> CreateVoucherEndpoint([AsParameters] VoucherServices services, SmartProductionPlan request, CancellationToken clt)
    {
        var command = new CreateProductPlanCommand(request);
        var result = await services.Mediator.Send(command, clt);
        return TypedResults.Ok(result);
    }
}

internal record CreateProductPlanCommand(SmartProductionPlan SmartProductionPlan) : ICommand<Result>;

internal class CreateProductPlanCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser) : ICommandHandler<CreateProductPlanCommand, Result>
{
    public async Task<Result> Handle(CreateProductPlanCommand command, CancellationToken clt)
    {
        var entityCreate = command.SmartProductionPlan.Adapt<Model.ProductPlant.SmartProductionPlan>();
        await dbContext.SmartProductionPlans.AddAsync(entityCreate, clt);

        entityCreate.CreateBy = currentUser.CodeUser;
        entityCreate.CodeUnit = currentUser.CodeUnit;
        if (entityCreate.SmartDataProductionPlans is { Count: > 0 })
        {
            entityCreate.SmartDataProductionPlans.ForEach(c => 
            {
                c.IdData = entityCreate.Id;
            });

            await dbContext.SmartDataProductionPlans.AddRangeAsync(entityCreate.SmartDataProductionPlans, clt);
        }
        int rowCount = 0;
        try
        {
            rowCount = await dbContext.SaveChangesAsync(clt);
        }
        catch (DbUpdateException ex)
        {

            foreach (var entry in ex.Entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                foreach (var prop in entry.CurrentValues.Properties)
                {
                    Console.WriteLine($"{prop.Name} = {entry.CurrentValues[prop]}");
                }
            }
            throw;
        }
        if (rowCount <= 0)
        {
            return Result.Failure(new Error("400", "Thêm mới thất bại"));
        }

        return Result.Success(entityCreate.Id);


    }

}