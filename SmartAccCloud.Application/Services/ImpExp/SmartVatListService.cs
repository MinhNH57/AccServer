using AutoMapper.QueryableExtensions;
using SmartAccCloud.Application.Interfaces.ImpExp;
using SmartAccCloud.Application.Models.SmartData;

namespace SmartAccCloud.Application.Services.ImpExp;

public class SmartVatListService(IApplicationDbContext content, IMapper mapper) : ISmartVatListService
{
    public async Task<Response.Result<List<SmartVatTaxVm>>> GetListVatByCouponId(Guid idContent,
        CancellationToken token)
    {
        var lstData = await content.SmartVatTaxList
            .AsNoTracking()
            .Where(c => c.IdContents == idContent)
            .ProjectTo<SmartVatTaxVm>(mapper.ConfigurationProvider)
            .ToListAsync(token)
            .ConfigureAwait(false);


        return Response.Result<List<SmartVatTaxVm>>.Success(lstData);
    }
}