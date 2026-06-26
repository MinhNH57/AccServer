using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Commands.Sales.DeleteSalesVoucher;

public record DeleteSalesVocherCommand(Guid Id) : ICommand<Result<bool>>;