using AutoMapper.QueryableExtensions;
using SmartAccCloud.Application.Interfaces.ImpExp;
using SmartAccCloud.Application.Models.SmartData;

namespace SmartAccCloud.Application.Services.ImpExp;

public class ImpExpService(IMapper mapper, IApplicationDbContext context) : IImpExpService
{
    /// <summary>
    /// Lấy dữ liệu phiếu nhập xuất theo Data type truyền vào
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns>Danh sách phiếu</returns>
    public async Task<Response.Result<List<SmartDataVm>>> GetImpOrExpCoupon(GetCouponRequest request,
        CancellationToken token)
    {
        var lstData = await context.SmartData
            .AsNoTracking()
            .Where(c => c.DataType == request.DataType && c.CodeUnit == request.CodeUnit)
            .ProjectTo<SmartDataVm>(mapper.ConfigurationProvider)
            .ToListAsync(token)
            .ConfigureAwait(false);


        return Response.Result<List<SmartDataVm>>.Success(lstData);
    }

    public async Task<Response.Result<List<SmartContentDataVm>>> GetDetailCoupon(Guid idContent,
        CancellationToken token)
    {
        var lstData = await context.SmartContentsData
            .AsNoTracking()
            .Where(c => c.IdContents == idContent)
            .ProjectTo<SmartContentDataVm>(mapper.ConfigurationProvider)
            .ToListAsync(token)
            .ConfigureAwait(false);

        return Response.Result<List<SmartContentDataVm>>.Success(lstData);
    }
}