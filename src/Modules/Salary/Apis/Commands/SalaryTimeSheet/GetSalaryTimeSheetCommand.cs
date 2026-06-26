using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Salary.Infrastructure;

namespace Salary.Apis.Commands.SalaryTimeSheet;

public record FindSalaryTimeSheetQuery(Guid Id) : IQuery<Result>;


public class FindSalaryTimeSheetQueryHandler(SalaryDbContext dbContext) : IQueryHandler<FindSalaryTimeSheetQuery, Result>
{
    public async Task<Result> Handle(FindSalaryTimeSheetQuery query, CancellationToken cancellationToken)
    {
        var salaryTimeSheet = await dbContext.SalaryTimeSheetHead.Include(x => x.SalaryTimeSheetDetails)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken)!;

        if (salaryTimeSheet is not null)
        {
            return Result.Success(salaryTimeSheet);
        }
        return Result.Failure(new Error("404", "Id salary not found.")); 
    }
}