using SmartAccCloud.Application.Models.Catalogs.CatalogProductForAsset;

namespace SmartAccCloud.Application.Services.Catalogs.ProductForAsset;
public class ProductForAssetServices(IApplicationDbContext context, IMapper mapper) : IProductForAssetServices
{
    public async Task<bool> CreateProductForAsset(List<CatalogProductForAssetDto> param)
    {
        List<CatalogProductForAsset> listAdd = mapper.Map<List<CatalogProductForAsset>>(param);

        await context.CatalogProductForAsset.AddRangeAsync(listAdd).ConfigureAwait(false);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> EditProductForAsset(List<CatalogProductForAssetDto> param)
    {
        //List cần sửa
        List<CatalogProductForAsset> listEdit = mapper.Map<List<CatalogProductForAsset>>(param);

        //Check xem có hàng hóa nào thuộc tài sản không.
        var lstEdit = await context.CatalogProductForAsset.AsNoTracking()
            .Where(x => x.CodeAsset == listEdit[0].CodeAsset).ToListAsync().ConfigureAwait(false);


        #region Xóa các content không có trong list sửa

        //Xóa các hàng hóa không có trong list truyền vào
        lstEdit.RemoveAll(item => listEdit.Any(x => x.Id == item.Id));
        if (lstEdit.Any())
        {
            context.CatalogProductForAsset.RemoveRange(lstEdit);
        }

        #endregion

        #region Sửa content mới và cũ

        foreach (var item in param)
        {
            var productEdit = await context.CatalogProductForAsset.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
            if (productEdit != null)
            {
                productEdit = mapper.Map(item, productEdit);
                context.CatalogProductForAsset.Update(productEdit);
            }
            else
            {
                var prdNew = new CatalogProductForAsset();
                prdNew = mapper.Map(item, prdNew);
                await context.CatalogProductForAsset.AddAsync(prdNew).ConfigureAwait(false);
            }
        }

        #endregion

        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> DeleteProductForAsset(List<CatalogProductForAssetDto> param)
    {
        List<CatalogProductForAsset> lstRemove = mapper.Map<List<CatalogProductForAsset>>(param);
        context.CatalogProductForAsset.RemoveRange(lstRemove);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }
}
