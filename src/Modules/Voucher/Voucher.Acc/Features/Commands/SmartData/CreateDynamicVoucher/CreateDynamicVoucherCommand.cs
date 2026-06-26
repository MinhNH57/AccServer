using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.SmartData.CreateDynamicVoucher;

public record CreateDynamicVoucherCommand(List<DynamicListDataModel> DynamicData) : ICommand<Result>;

