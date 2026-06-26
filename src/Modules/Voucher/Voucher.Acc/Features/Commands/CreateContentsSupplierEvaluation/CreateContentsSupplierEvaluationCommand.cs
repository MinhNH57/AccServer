using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using FluentValidation;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.CreateContentsSupplierEvaluation;

public class CreateContentsSupplierEvaluationRequest
{
    public Guid IdVoucher { get; set; }
    public List<SmartContentsSupplierEvaluation> SmartContentsSupplierEvaluations { get; set; } = new();
}

public class CreateContentsSupplierEvaluationValidator : AbstractValidator<CreateContentsSupplierEvaluationCommand>
{
    public CreateContentsSupplierEvaluationValidator()
    {
        RuleFor(c => c.CreateContentsSupplierEvaluation.SmartContentsSupplierEvaluations)
            .NotNull()
            .WithMessage("Nội dung đánh giá là bắt buộc");
    }
}

public record CreateContentsSupplierEvaluationCommand(CreateContentsSupplierEvaluationRequest CreateContentsSupplierEvaluation) : ICommand<Result>;

public class CreateContentsSupplierEvaluationCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser)
    : ICommandHandler<CreateContentsSupplierEvaluationCommand, Result>
{
    public async Task<Result> Handle(CreateContentsSupplierEvaluationCommand command, CancellationToken cancellationToken)
    {
        command.CreateContentsSupplierEvaluation.SmartContentsSupplierEvaluations.ForEach(c =>
        {
            c.IdContents = command.CreateContentsSupplierEvaluation.IdVoucher;
            c.CodeUnit = currentUser.CodeUnit;
        });

        await dbContext.SmartContentsSupplierEvaluation.AddRangeAsync(command.CreateContentsSupplierEvaluation
            .SmartContentsSupplierEvaluations, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success("Create data successfully");
    }
}