using SmartAccCloud.Application.Models.Catalogs.CatalogProductForContract;

namespace SmartAccCloud.Application.Services.Catalogs.ProductForContract;

public class ProductForContractServices(IApplicationDbContext context, IMapper mapper) : IProductForContractServices
{
    public async Task<bool> CreateProductForContract(List<ProductForContractDto> param)
    {
        List<CatalogProductForContract> listAdd = mapper.Map<List<CatalogProductForContract>>(param);

        await context.CatalogProductForContract.AddRangeAsync(listAdd).ConfigureAwait(false);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> EditProductForContract(List<ProductForContractDto> param)
    {
        //List cần sửa
        List<CatalogProductForContract> listEdit = mapper.Map<List<CatalogProductForContract>>(param);

        //Check xem có hàng hóa nào thuộc hợp đồng không.
        var lstEdit = await context.CatalogProductForContract.AsNoTracking()
            .Where(x => x.CodeContract == listEdit[0].CodeContract).ToListAsync().ConfigureAwait(false);

        #region Xóa các content không có trong list sửa

        //Xóa các hàng hóa không có trong list truyền vào
        lstEdit.RemoveAll(item => listEdit.Any(x => x.Id == item.Id));
        if (lstEdit.Any())
        {
            context.CatalogProductForContract.RemoveRange(lstEdit);
        }

        #endregion

        #region Sửa content mới và cũ

        foreach (var item in param)
        {
            var productEdit = await context.CatalogProductForContract.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
            if (productEdit != null)
            {
                productEdit = mapper.Map(item, productEdit);
                context.CatalogProductForContract.Update(productEdit);
            }
            else
            {
                var prdNew = new CatalogProductForContract();
                prdNew = mapper.Map(item, prdNew);
                await context.CatalogProductForContract.AddAsync(prdNew).ConfigureAwait(false);
            }
        }

        #endregion

        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> DeleteProductForContract(List<ProductForContractDto> param)
    {
        List<CatalogProductForContract> lstRemove = mapper.Map<List<CatalogProductForContract>>(param);

        context.CatalogProductForContract.RemoveRange(lstRemove);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }
}