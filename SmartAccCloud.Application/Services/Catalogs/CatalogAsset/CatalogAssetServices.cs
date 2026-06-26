using SmartAccCloud.Application.Models.Catalogs.CatalogAsset;

namespace SmartAccCloud.Application.Services.Catalogs.CatalogAsset;

public class CatalogAssetServices(IApplicationDbContext context, IMapper mapper) : ICatalogAssetServices
{
    public async Task<Result<bool>> CreateAsset(CatalogAssetDto param)
    {
        bool existsByCode = await context.CatalogAsset.AsNoTracking()
            .AnyAsync(x => x.AssetCode == param.AssetCode)
            .ConfigureAwait(false);
        if (existsByCode)
            return Result<bool>.Failure(new Error("400", "Mã tài sản đã tồn tại"));

        try
        {
            Domain.Entity.Catalogs.CatalogAsset modelCreate = new();
            modelCreate = mapper.Map(param, modelCreate);

            await context.CatalogAsset.AddAsync(modelCreate).ConfigureAwait(false);
            int count = await context.SaveChangesAsync().ConfigureAwait(false);

            if (count > 0)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(new Error("400", "Thêm thất bại"));
        }
        catch (Exception e)
        {
            return Result<bool>.Failure(new Error("500", e.Message));
        }
    }

    public async Task<Result<bool>> EditAsset(CatalogAssetDto param)
    {
        var assetEdit = await context.CatalogAsset
            .FirstOrDefaultAsync(x => x.AssetCode == param.AssetCode).ConfigureAwait(false);
        if (assetEdit == null)
            return Result<bool>.Failure(new Error("400", "Sửa thất bại do mã tài sản không tồn tại"));

        try
        {
            assetEdit = mapper.Map(param, assetEdit);
            context.CatalogAsset.Update(assetEdit);
            int count = await context.SaveChangesAsync().ConfigureAwait(false);

            if (count > 0)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(new Error("400", "Cập nhật thất bại"));
        }
        catch (Exception e)
        {
            return Result<bool>.Failure(new Error("500", e.Message));
        }
    }
}
