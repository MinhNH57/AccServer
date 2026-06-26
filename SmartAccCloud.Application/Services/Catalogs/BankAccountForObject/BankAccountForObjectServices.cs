using SmartAccCloud.Application.Models.Catalogs.BankAccountForObj;

namespace SmartAccCloud.Application.Services.Catalogs.BankAccountForObject;
public class BankAccountForObjectServices(IApplicationDbContext context, IMapper mapper) : IBankAccountForObjectServices
{
    public async Task<bool> CreateBankAccountForObject(List<BankAccountForObjectDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogBankAccountForObject> listAdd = mapper.Map<List<Domain.Entity.Catalogs.CatalogBankAccountForObject>>(param);

        await context.CatalogBankAccountForObject.AddRangeAsync(listAdd).ConfigureAwait(false);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> EditBankAccountForObject(List<BankAccountForObjectDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogBankAccountForObject> listEdit = mapper.Map<List<Domain.Entity.Catalogs.CatalogBankAccountForObject>>(param);

        //Check xem có bank nào thuộc đơn vị không.
        var lstEdit = await context.CatalogBankAccountForObject.AsNoTracking()
            .Where(x => x.Id == listEdit[0].Id).ToListAsync().ConfigureAwait(false);


        #region Xóa các content không có trong list sửa

        //Xóa các qr không có trong list truyền vào
        lstEdit.RemoveAll(item => listEdit.Any(x => x.Id == item.Id));
        if (lstEdit.Any())
        {
            context.CatalogBankAccountForObject.RemoveRange(lstEdit);
        }

        #endregion

        #region Sửa content mới và cũ

        foreach (var item in param)
        {
            var bankEdit = await context.CatalogBankAccountForObject.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
            if (bankEdit != null)
            {
                bankEdit = mapper.Map(item, bankEdit);
                context.CatalogBankAccountForObject.Update(bankEdit);
            }
            else
            {
                var prdNew = new Domain.Entity.Catalogs.CatalogBankAccountForObject();
                prdNew = mapper.Map(item, prdNew);
                await context.CatalogBankAccountForObject.AddAsync(prdNew).ConfigureAwait(false);
            }
        }

        #endregion

        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> DeleteBankAccountForObject(List<BankAccountForObjectDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogBankAccountForObject> lstRemove = mapper.Map<List<Domain.Entity.Catalogs.CatalogBankAccountForObject>>(param);

        context.CatalogBankAccountForObject.RemoveRange(lstRemove);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }
}
