using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Commands.SmartDataProductPlan.UpdateProductPlan;

public class UpdateProductPlanRequest
{
    public Guid Id { get; set; }
    public Model.ProductPlant.SmartProductionPlan SmartProductionPlan { get; set; }
}

public record UpdateProductPlanCommand(UpdateProductPlanRequest Request) : ICommand<Result<bool>>;