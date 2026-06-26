using SmartAccCloud.Application.Models.Catalogs.CatalogDependents;

namespace SmartAccCloud.Application.Services.Catalogs.Dependents;
public class DependentsServices(IApplicationDbContext context, IMapper mapper) : IDependentsServices
{
    public async Task<bool> CreateDependents(List<DependentsDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogDependents> listAdd = mapper.Map<List<Domain.Entity.Catalogs.CatalogDependents>>(param);

        await context.CatalogDependents.AddRangeAsync(listAdd).ConfigureAwait(false);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> EditDependents(List<DependentsDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogDependents> listEdit = mapper.Map<List<Domain.Entity.Catalogs.CatalogDependents>>(param);

        //Check xem có người giám hộ nào thuộc đơn vị không.
        var lstEdit = await context.CatalogDependents.AsNoTracking()
            .Where(x => x.Id == listEdit[0].Id).ToListAsync().ConfigureAwait(false);


        #region Xóa các content không có trong list sửa

        //Xóa các qr không có trong list truyền vào
        lstEdit.RemoveAll(item => listEdit.Any(x => x.Id == item.Id));
        if (lstEdit.Any())
        {
            context.CatalogDependents.RemoveRange(lstEdit);
        }

        #endregion

        #region Sửa content mới và cũ

        foreach (var item in param)
        {
            var dependentEdit = await context.CatalogDependents.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
            if (dependentEdit != null)
            {
                dependentEdit = mapper.Map(item, dependentEdit);
                context.CatalogDependents.Update(dependentEdit);
            }
            else
            {
                var prdNew = new Domain.Entity.Catalogs.CatalogDependents();
                prdNew = mapper.Map(item, prdNew);
                await context.CatalogDependents.AddAsync(prdNew).ConfigureAwait(false);
            }
        }

        #endregion

        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }

    public async Task<bool> DeleteDependents(List<DependentsDto> param)
    {
        List<Domain.Entity.Catalogs.CatalogDependents> lstRemove = mapper.Map<List<Domain.Entity.Catalogs.CatalogDependents>>(param);

        context.CatalogDependents.RemoveRange(lstRemove);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);
        bool result = count > 0;
        if (result)
            return true;
        return false;
    }
}
