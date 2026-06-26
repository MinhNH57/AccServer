using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Salary.Infrastructure;
using Salary.Model;

namespace Salary.Apis.Commands;

public record UpdateSalaryTimeSheetCommand(
    Guid IdSalaySummaryOfTimekeeping,
    List<Model.SalaryTimeSheet> LstSalaryTimeSheet) : ICommand<Result>;

public class UpdateSalaryTimeSheetCommandHandler(SalaryDbContext dbContext) : ICommandHandler<UpdateSalaryTimeSheetCommand, Result>
{
    public async Task<Result> Handle(UpdateSalaryTimeSheetCommand command, CancellationToken cancellationToken)
    {
        var lstData = await dbContext.SalaryTimeSheet
            .Where(x => x.IdSalaySummaryOfTimekeeping == command.IdSalaySummaryOfTimekeeping)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
        //Nếu bảng lương trống thì xóa.
        if (!command.LstSalaryTimeSheet.Any())
        {
            dbContext.SalaryTimeSheet.RemoveRange(lstData);
        }
        else
        {
            //Lấy ra bảng lương mới cần tạo
            var lstNew = command.LstSalaryTimeSheet
                .Where(qItem => lstData.All(dataItem => dataItem.Id != qItem.Id))
                .ToList();

            // 2. Lấy danh sách các bảng lương đã xóa
            var lstRemove = lstData
                .Where(dataItem => command.LstSalaryTimeSheet.All(qItem => qItem.Id != dataItem.Id))
                .ToList();

            // Xóa bảng lương đã chọn
            if (lstRemove.Any())
            {
                dbContext.SalaryTimeSheet.RemoveRange(lstRemove);
            }

            // Lọc các đối tượng có trong listEdit nhưng không có trong lstNew và lstRemove
            foreach (var item in command.LstSalaryTimeSheet
                         .Where(qItem => lstNew.All(newItem => newItem.Id != qItem.Id) && lstRemove.All(removeItem => removeItem.Id != qItem.Id)))
            {
                dbContext.SalaryTimeSheet.Update(item);
            }

            // Tạo bảng lương mới
            if (lstNew.Any())
            {
                await dbContext.SalaryTimeSheet.AddRangeAsync(lstNew, cancellationToken);
            }

        }
        // Lưu thay đổi vào database
        var count = await dbContext.SaveChangesAsync(cancellationToken);

        if (count > 0)
        {
            return Result.Success(true); // Trả về kết quả thành công

        }
        return Result.Failure(new Error("400", "Sửa thất bại"));
    }
}