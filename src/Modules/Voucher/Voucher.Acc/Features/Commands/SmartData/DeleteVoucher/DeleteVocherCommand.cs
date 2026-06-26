using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Commands.SmartData.DeleteVoucher;

public record DeleteVocherCommand(Guid Id, string TableName) : ICommand<Result>;