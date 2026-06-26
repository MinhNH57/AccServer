using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.Sales.CreateSalesVoucher;

public record CreateSalesVoucherCommand(List<DynamicListDataModel> DynamicData) : ICommand<Result<string>>;

