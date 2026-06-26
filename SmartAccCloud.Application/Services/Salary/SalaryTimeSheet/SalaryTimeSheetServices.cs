using AutoMapper.QueryableExtensions;
using SmartAccCloud.Application.Models.Salary.SalaryTimeSheet;

namespace SmartAccCloud.Application.Services.Salary.SalaryTimeSheet;
public class SalaryTimeSheetServices(IApplicationDbContext context, IMapper mapper) : ISalaryTimeSheetServices
{
    public async Task<Result<bool>> CreateSalaryTimeSheet(List<SalaryTimeSheetDto> query)
    {

        List<Domain.Entity.Salary.SalaryTimeSheet> lstAdd =
            mapper.Map<List<Domain.Entity.Salary.SalaryTimeSheet>>(query);

        // Thêm danh sách vào cơ sở dữ liệu
        await context.SalaryTimeSheet.AddRangeAsync(lstAdd);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);

        if (count > 0)
        {
            return Result<bool>.Success(true);
        }

        return Result<bool>.Failure(new Error("400", "Thêm thất bại"));

    }

    public async Task<List<SalaryTimeSheetDto>> GetSalaryTimeSheetById(Guid id)
    {
        var lstData = await context.SalaryTimeSheet
            .Where(x => x.IdSalaySummaryOfTimekeeping == id)
            .AsNoTracking()
            .ProjectTo<SalaryTimeSheetDto>(mapper.ConfigurationProvider).ToListAsync()
            .ConfigureAwait(false);
        return lstData;
    }

    public async Task<Result<bool>> EditSalaryTimeSheet(SalaryTimeSheetQuery query)
    {
        List<Domain.Entity.Salary.SalaryTimeSheet> listEdit = mapper.Map<List<Domain.Entity.Salary.SalaryTimeSheet>>(query.LstSalaryTimeSheet);
        var lstData = await context.SalaryTimeSheet
            .Where(x => x.IdSalaySummaryOfTimekeeping == query.IdSalaySummaryOfTimekeeping)
            .AsNoTracking()
            .ToListAsync();
        //Nếu bảng lương trống thì xóa.
        if (!query.LstSalaryTimeSheet.Any())
        {
            context.SalaryTimeSheet.RemoveRange(lstData);
        }
        else
        {
            //Lấy ra bảng lương mới cần tạo
            var lstNew = listEdit
                .Where(qItem => lstData.All(dataItem => dataItem.Id != qItem.Id))
                .ToList();

            // 2. Lấy danh sách các bảng lương đã xóa
            var lstRemove = lstData
                .Where(dataItem => listEdit.All(qItem => qItem.Id != dataItem.Id))
                .ToList();
    
            // Xóa bảng lương đã chọn
            if (lstRemove.Any())
            {
                context.SalaryTimeSheet.RemoveRange(lstRemove);
            }

            // Lọc các đối tượng có trong listEdit nhưng không có trong lstNew và lstRemove
            foreach (var item in listEdit
                         .Where(qItem => lstNew.All(newItem => newItem.Id != qItem.Id) && lstRemove.All(removeItem => removeItem.Id != qItem.Id)))
            {
                 context.SalaryTimeSheet.Update(item);

            }

            // Tạo bảng lương mới
            if (lstNew.Any())
            {
                await context.SalaryTimeSheet.AddRangeAsync(lstNew);
            }

        }
        // Lưu thay đổi vào database
        var count = await context.SaveChangesAsync();

        if (count > 0)
        {
            return Result<bool>.Success(true); // Trả về kết quả thành công

        }
        return Result<bool>.Failure(new Error("400", "Sửa thất bại"));

    }

}
