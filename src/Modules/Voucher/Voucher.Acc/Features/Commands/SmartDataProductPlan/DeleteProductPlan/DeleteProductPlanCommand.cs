using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Commands.SmartDataProductPlan.DeleteProductPlan;

public record DeleteProductPlanCommand(Guid Id, string TableName) : ICommand<Result>;