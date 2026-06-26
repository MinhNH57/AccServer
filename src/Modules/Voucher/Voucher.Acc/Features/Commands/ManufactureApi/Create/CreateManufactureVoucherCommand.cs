using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.ManufactureApi.Create;

public record CreateManufactureVoucherCommand(List<DynamicListDataModel> DynamicData) : ICommand<Result<bool>>;

