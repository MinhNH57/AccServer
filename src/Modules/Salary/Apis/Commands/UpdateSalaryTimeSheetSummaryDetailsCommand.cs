using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Salary.Infrastructure;
using Salary.Model;

namespace Salary.Apis.Commands;

public record UpdateSalaryTimeSheetSummaryDetailsCommand(
    Guid IdSalaryTimeSheetSummary,
    List<SalaryTimeSheetSummaryDetails> LstSalaryTimeSheetSummaryDetails
    ) : ICommand<Result>;


public class UpdateSalaryTimeSheetSummaryDetailsCommandHandler(SalaryDbContext dbContext) : ICommandHandler<UpdateSalaryTimeSheetSummaryDetailsCommand, Result>
{
    public async Task<Result> Handle(UpdateSalaryTimeSheetSummaryDetailsCommand command, CancellationToken cancellationToken)
    {
        var lstData = await dbContext.SalaryTimeSheetSummaryDetails
            .Where(x => x.IdSalaryTimeSheetSummary == command.IdSalaryTimeSheetSummary)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
        //Nếu bảng lương trống thì xóa.
        if (!command.LstSalaryTimeSheetSummaryDetails.Any())
        {
            dbContext.SalaryTimeSheetSummaryDetails.RemoveRange(lstData);
        }
        else
        {
            //Lấy ra bảng lương mới cần tạo
            var lstNew = command.LstSalaryTimeSheetSummaryDetails
                .Where(qItem => lstData.All(dataItem => dataItem.Id != qItem.Id))
                .ToList();

            // 2. Lấy danh sách các bảng lương đã xóa
            var lstRemove = lstData
                .Where(dataItem => command.LstSalaryTimeSheetSummaryDetails.All(qItem => qItem.Id != dataItem.Id))
                .ToList();

            // Xóa bảng lương đã chọn
            if (lstRemove.Any())
            {
                dbContext.SalaryTimeSheetSummaryDetails.RemoveRange(lstRemove);
            }

            // Lọc các đối tượng có trong listEdit nhưng không có trong lstNew và lstRemove
            foreach (var item in command.LstSalaryTimeSheetSummaryDetails
                         .Where(qItem => lstNew.All(newItem => newItem.Id != qItem.Id) && lstRemove.All(removeItem => removeItem.Id != qItem.Id)))
            {
                dbContext.SalaryTimeSheetSummaryDetails.Update(item);

            }

            // Tạo bảng lương mới
            if (lstNew.Any())
            {
                await dbContext.SalaryTimeSheetSummaryDetails.AddRangeAsync(lstNew, cancellationToken);
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