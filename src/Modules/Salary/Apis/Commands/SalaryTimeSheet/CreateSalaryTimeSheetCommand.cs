using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Mapster;
using Salary.Dto.SalaryTimeSheet;
using Salary.Infrastructure;
using Salary.Model;

namespace Salary.Apis.Commands.SalaryTimeSheet;
public record CreateSalaryTimeSheetCommand( 
    SalaryTimeSheetRequest CreateData) : ICommand<Result>;

public class CreateSalaryTimeSheetCommandHandler(SalaryDbContext dbContext, ICurrentUser currentUser) : ICommandHandler<CreateSalaryTimeSheetCommand, Result>
{
    public async Task<Result> Handle(CreateSalaryTimeSheetCommand command, CancellationToken cancellationToken)
    {  
        var headCreate = command.CreateData.SalaryHead.Adapt<SalaryTimeSheetHead>();
        var contCreate = command.CreateData.SalaryDetail.Adapt<List<SalaryTimeSheetDetail>>();
        headCreate.CodeUnit = currentUser.CodeUnit;

        contCreate.ForEach(x => x.IdSalaryHead = headCreate.Id);

        contCreate.ForEach(x =>
        {
            x.IdSalaryHead = headCreate.Id;
            x.CodeUnit = currentUser.CodeUnit;
        });
        headCreate.TotalNetIncomeAmount = contCreate.Sum(x => x.TotalSalaryPaid);
        if (!contCreate.Any()) return Result.Failure(new Error("400", "Nội dung bảng lương không được bỏ trống")); 
        await dbContext.SalaryTimeSheetHead.AddAsync(headCreate, cancellationToken);
        await dbContext.SalaryTimeSheetDetail.AddRangeAsync(contCreate, cancellationToken); 
        // Lưu thay đổi vào database
        var count = await dbContext.SaveChangesAsync(cancellationToken);

        if (count > 0)
        {
            return Result.Success(headCreate.Id);   
        }
        return Result.Failure(new Error("400", "Sửa thất bại"));
    }
}