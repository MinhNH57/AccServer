using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.ManufactureApi.Update;

public class UpdateManufactureRequest
{
    public Guid Id { get; set; }
    public SmartDataManufacture SmartDataManufacture { get; set; }
}

public record UpdateManufactureCommand(UpdateManufactureRequest Request) : ICommand<Result<bool>>;