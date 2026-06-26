using SmartAccCloud.Application.Models.Catalogs.BillOfMaterials;

namespace SmartAccCloud.Application.Services.Catalogs.BillOfMaterials;
public class BillOfMaterialsServices(IApplicationDbContext context, IMapper mapper) :IBillOfMaterialsServices
{
    public async Task<bool> CreateBillOfMaterials(List<BillOfMaterialsDto> param)
    {
        List<Domain.Entity.Catalogs.BillOfMaterials> listAdd = mapper.Map<List<Domain.Entity.Catalogs.BillOfMaterials>>(param);

        await context.BillOfMaterials.AddRangeAsync(listAdd).ConfigureAwait(false);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> EditBillOfMaterials(List<BillOfMaterialsDto> param)
    {
        List<Domain.Entity.Catalogs.BillOfMaterials> listEdit = mapper.Map<List<Domain.Entity.Catalogs.BillOfMaterials>>(param);

        //Check xem có hàng hóa nào thuộc tài sản không.
        var lstEdit = await context.BillOfMaterials.AsNoTracking()
            .Where(x => x.ProductCode == listEdit[0].ProductCode).ToListAsync().ConfigureAwait(false);


        #region Xóa các content không có trong list sửa

        //Xóa các hàng hóa không có trong list truyền vào
        lstEdit.RemoveAll(item => listEdit.Any(x => x.Id == item.Id));
        if (lstEdit.Any())
        {
            context.BillOfMaterials.RemoveRange(lstEdit);
        }

        #endregion

        #region Sửa content mới và cũ

        foreach (var item in param)
        {
            var productEdit = await context.BillOfMaterials.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
            if (productEdit != null)
            {
                productEdit = mapper.Map(item, productEdit);
                context.BillOfMaterials.Update(productEdit);
            }
            else
            {
                var prdNew = new Domain.Entity.Catalogs.BillOfMaterials();
                prdNew = mapper.Map(item, prdNew);
                await context.BillOfMaterials.AddAsync(prdNew).ConfigureAwait(false);
            }
        }

        #endregion

        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> DeleteBillOfMaterials(List<BillOfMaterialsDto> param)
    {
        List<Domain.Entity.Catalogs.BillOfMaterials> lstRemove = mapper.Map<List<Domain.Entity.Catalogs.BillOfMaterials>>(param);
         context.BillOfMaterials.RemoveRange(lstRemove);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }
}
