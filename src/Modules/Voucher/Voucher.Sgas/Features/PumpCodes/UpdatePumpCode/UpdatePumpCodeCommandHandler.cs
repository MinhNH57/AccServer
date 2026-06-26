using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using Microsoft.EntityFrameworkCore;
using Voucher.Sgas.Entities;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.SmartDataPumpCode;

namespace Voucher.Sgas.Features.PumpCodes.UpdatePumpCode;


public record class UpdatePumpCodeCommand(UpdatePumpCodeRequest Request) : IQuery<Result>;
// Regular CommandHandler
public class UpdatePumpCodeCommandHandler(VoucherSgasDbContext dbContext, IMappingService mapping) : IQueryHandler<UpdatePumpCodeCommand, Result>
{
    public async Task<Result> Handle(UpdatePumpCodeCommand request, CancellationToken cancellationToken)
    {
        var pumpCodes = await dbContext.SmartDataPumpCode
            .AsNoTracking()
            .Where(c => request.Request.Ids.Contains(c.Id))
            .AsNoTracking().ToListAsync(cancellationToken);

        if (pumpCodes is not { Count: > 0 })
        {
            return Result.Failure(new Error(ErrorCode.NotFound, "Không tìm thấy mã bơm: " + string.Join(",", request.Request.Ids)));
        }

        foreach (var pumpCode in pumpCodes)
        {
            mapping.MapExistingModels(request.Request, pumpCode);
        }
        dbContext.SmartDataPumpCode.UpdateRange(pumpCodes);
        var count = await dbContext.SaveChangesAsync(cancellationToken);

        if (count > 0)
        {   
            var dto = mapping.MapList<SmartDataPumpCode, PumpCodeDTO>(pumpCodes); 
            return Result.Success(dto);
        }
        return Result.Failure(new Error(ErrorCode.InternalServerError, "Có lỗi trong quá trình cất giữ."));

    }
}

