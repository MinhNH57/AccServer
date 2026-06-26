using AutoMapper.QueryableExtensions;
using SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummaryDetails;

namespace SmartAccCloud.Application.Services.Salary.SalaryTimeSheetSummaryDetails;
public class SalaryTimeSheetSummaryDetailsServices(IApplicationDbContext context, IMapper mapper) : ISalaryTimeSheetSummaryDetailsServices
{
    public async Task<Result<bool>> CreateSalaryTimeSheetSummaryDetails(List<SalaryTimeSheetSummaryDetailsDto> query)
    {

        List<Domain.Entity.Salary.SalaryTimeSheetSummaryDetails> lstAdd =
            mapper.Map<List<Domain.Entity.Salary.SalaryTimeSheetSummaryDetails>>(query);

        // Thêm danh sách vào cơ sở dữ liệu
        await context.SalaryTimeSheetSummaryDetails.AddRangeAsync(lstAdd);
        int count = await context.SaveChangesAsync().ConfigureAwait(false);

        if (count > 0)
        {
            return Result<bool>.Success(true);
        }

        return Result<bool>.Failure(new Error("400", "Thêm thất bại"));

    }


    public async Task<List<SalaryTimeSheetSummaryDetailsDto>> GetSalaryTimeSheetSummaryDetailsById(Guid id)
    {
        var lstData = await context.SalaryTimeSheetSummaryDetails
            .Where(x => x.IdSalaryTimeSheetSummary == id)
            .AsNoTracking()
            .ProjectTo<SalaryTimeSheetSummaryDetailsDto>(mapper.ConfigurationProvider).ToListAsync()
            .ConfigureAwait(false);
        return lstData;
    }

    public async Task<Result<bool>> EditSalaryTimeSheetSummaryDetails(SalaryTimeSheetSummaryDetailsQuery query)
    {
        List<Domain.Entity.Salary.SalaryTimeSheetSummaryDetails> listEdit = mapper.Map<List<Domain.Entity.Salary.SalaryTimeSheetSummaryDetails>>(query.LstSalaryTimeSheetSummaryDetails);
        var lstData = await context.SalaryTimeSheetSummaryDetails
            .Where(x => x.IdSalaryTimeSheetSummary == query.IdSalaryTimeSheetSummary)
            .AsNoTracking()
            .ToListAsync();
        //Nếu bảng lương trống thì xóa.
        if (!query.LstSalaryTimeSheetSummaryDetails.Any())
        {
            context.SalaryTimeSheetSummaryDetails.RemoveRange(lstData);
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
                context.SalaryTimeSheetSummaryDetails.RemoveRange(lstRemove);
            }

            // Lọc các đối tượng có trong listEdit nhưng không có trong lstNew và lstRemove
            foreach (var item in listEdit
                         .Where(qItem => lstNew.All(newItem => newItem.Id != qItem.Id) && lstRemove.All(removeItem => removeItem.Id != qItem.Id)))
            {
                 context.SalaryTimeSheetSummaryDetails.Update(item);

            }

            // Tạo bảng lương mới
            if (lstNew.Any())
            {
                await context.SalaryTimeSheetSummaryDetails.AddRangeAsync(lstNew);
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
