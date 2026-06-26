using SmartAccCloud.Application.Models.Catalogs.QrCodeProduct;

namespace SmartAccCloud.Application.Services.Catalogs.QrCodeForProduct;
internal class QrCodeForProductServices(IApplicationDbContext context, IMapper mapper) :IQrCodeForProductServices
{
    public async Task<bool> CreateQrCodeProduct(List<QrCodeProductDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogQrCodeProduct> listAdd = mapper.Map<List<Domain.Entity.Catalogs.CatalogQrCodeProduct>>(param);

        await context.CatalogQrCodeProduct.AddRangeAsync(listAdd).ConfigureAwait(false);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> EditQrCodeProduct(List<QrCodeProductDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogQrCodeProduct> listEdit = mapper.Map<List<Domain.Entity.Catalogs.CatalogQrCodeProduct>>(param);

        //Check xem có qr nào thuộc hàng hóa không.
        var lstEdit = await context.CatalogQrCodeProduct.AsNoTracking()
            .Where(x => x.ProductCode == listEdit[0].ProductCode).ToListAsync().ConfigureAwait(false);


        #region Xóa các content không có trong list sửa

        //Xóa các qr không có trong list truyền vào
        lstEdit.RemoveAll(item => listEdit.Any(x => x.Id == item.Id));
        if (lstEdit.Any())
        {
            context.CatalogQrCodeProduct.RemoveRange(lstEdit);
        }

        #endregion

        #region Sửa content mới và cũ

        foreach (var item in param)
        {
            var productEdit = await context.CatalogQrCodeProduct.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
            if (productEdit != null)
            {
                productEdit = mapper.Map(item, productEdit);
                context.CatalogQrCodeProduct.Update(productEdit);
            }
            else
            {
                var prdNew = new Domain.Entity.Catalogs.CatalogQrCodeProduct();
                prdNew = mapper.Map(item, prdNew);
                await context.CatalogQrCodeProduct.AddAsync(prdNew).ConfigureAwait(false);
            }
        }

        #endregion

        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> DeleteQrCodeProduct(List<QrCodeProductDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogQrCodeProduct> lstRemove = mapper.Map<List<Domain.Entity.Catalogs.CatalogQrCodeProduct>>(param);

        context.CatalogQrCodeProduct.RemoveRange(lstRemove);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }
}
