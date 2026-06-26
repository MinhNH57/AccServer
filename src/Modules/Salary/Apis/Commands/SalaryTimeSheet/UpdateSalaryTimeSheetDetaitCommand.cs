using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Salary.Dto.SalaryTimeSheet;
using Salary.Infrastructure;
using Salary.Model;

namespace Salary.Apis.Commands.SalaryTimeSheet;

public record UpdateSalaryTimeSheetDetaitCommand(
    SalaryTimeSheetRequest UpdateData) : ICommand<Result>;


public class UpdateSalaryTimeSheetDetaitCommandHandler(SalaryDbContext dbContext, ICurrentUser currentUser) : ICommandHandler<UpdateSalaryTimeSheetDetaitCommand, Result>
{
    public async Task<Result> Handle(UpdateSalaryTimeSheetDetaitCommand command, CancellationToken cancellationToken)
    {
        var headReqest = command.UpdateData.SalaryHead.Adapt<SalaryTimeSheetHead>();
        var contentRequest = command.UpdateData.SalaryDetail.Adapt<List<SalaryTimeSheetDetail>>();

        var headSalary = dbContext.SalaryTimeSheetHead.FirstOrDefault(x => x.Id == headReqest.Id);
        if (headSalary is null) return Result.Failure(new Error("400", "Không tồn tại bảng lương này."));


        var lstSalary = await dbContext.SalaryTimeSheetDetail.Where(x => x.IdSalaryHead == headReqest.Id).AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);



        var lstCreate = contentRequest!
            .Where(x => lstSalary.All(y => y.ObjectCode != x.ObjectCode))
            .ToList();

        var lstUpdate = contentRequest
            .Where(c => lstSalary.Any(r => r.ObjectCode.ToString() == c.ObjectCode))
            .ToList();

        var lstRemove = lstSalary
            .Where(y => contentRequest!.All(x => x.ObjectCode != y.ObjectCode))
            .ToList();

        lstCreate.ForEach(x =>
        {
            x.IdSalaryHead = headReqest.Id;
            x.CodeUnit = currentUser.CodeUnit;
        });
         

        headSalary.TotalNetIncomeAmount = contentRequest.Sum(x => x.TotalSalaryPaid);



        dbContext.SalaryTimeSheetHead.Update(headSalary);


        await dbContext.SalaryTimeSheetDetail.AddRangeAsync(lstCreate, cancellationToken);

        dbContext.SalaryTimeSheetDetail.UpdateRange(lstUpdate);

        dbContext.SalaryTimeSheetDetail.RemoveRange(lstRemove);

         
        // Lưu thay đổi vào database
        var count = await dbContext.SaveChangesAsync(cancellationToken);

        if (count > 0)
        {
            return Result.Success(headReqest.Id);
        }
        return Result.Failure(new Error("400", "Sửa thất bại"));
    }
}