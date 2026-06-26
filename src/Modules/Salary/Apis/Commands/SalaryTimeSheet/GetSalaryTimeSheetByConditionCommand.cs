using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Salary.Infrastructure;
using Salary.Model;
using Salary.Request;

namespace Salary.Apis.Commands.SalaryTimeSheet;


public record GetSalaryTimeSheetByConditionCommand(GetSalaryTimeSheetByCondition Request) : IQuery<Result>;


public class GetSalaryTimeSheetByConditionCommandnHandler(SalaryDbContext dbContext) : IQueryHandler<GetSalaryTimeSheetByConditionCommand, Result>
{ 
    public async Task<Result> Handle(GetSalaryTimeSheetByConditionCommand request, CancellationToken cancellationToken)
    {
        var salaryTimeSheet = await dbContext.SalaryTimeSheetHead.Include(x => x.SalaryTimeSheetDetails)
            .FirstOrDefaultAsync(x => x.PeriodMonth == request.Request.PeriodMonth
                                      && x.PeriodYear == request.Request.PeriodYear
                && x.CodeTypeSalary == request.Request.CodeTypeSalary
                && x.CodeRoom == request.Request.CodeRoom, cancellationToken: cancellationToken)!;

        if (salaryTimeSheet is not null)
        {
            return Result.Success(salaryTimeSheet);
        }
        return Result.Success(new SalaryTimeSheetHead(),new ("404","Không tồn tại bảng lương với điều kiện này"));
    }
}