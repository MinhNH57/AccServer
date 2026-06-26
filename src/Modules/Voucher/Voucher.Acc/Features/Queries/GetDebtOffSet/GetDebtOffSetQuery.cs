using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Response;
using Voucher.Acc.Model;
using Voucher.Acc.Model.DebtOffSet;

namespace Voucher.Acc.Features.Queries.GetDebtOffSet;


public record GetDebtOffSetQuery(Guid IdVoucher) : IQuery<Result<List<SmartDebtOffSetContents>>>
{

}
public class UpdateDebtOffSetRequest
{
    public Guid Id { get; set; }
    public SmartDebtOffSet SmartDebtOffSet { get; set; }
}

public record UpdateDebtOffSetCommand(UpdateDebtOffSetRequest Request) : ICommand<Result<bool>>;