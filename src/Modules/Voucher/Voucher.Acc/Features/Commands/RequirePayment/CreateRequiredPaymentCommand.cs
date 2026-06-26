using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Voucher.Acc.Features.Commands.ConfirmVoucher;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.RequirePayment;

public record CreateRequiredPaymentCommand(CreateRequiPaymentDataModel CreateModel) : ICommand<Result>;

public class CreateRequiredPaymentCommandHandle(VoucherDbContext dbContext, ICurrentUser currentUser, IMediator mediator) : ICommandHandler<CreateRequiredPaymentCommand, Result>
{
    public async Task<Result> Handle(CreateRequiredPaymentCommand command, CancellationToken cancellationToken)
    {

        var entityCreate = command.CreateModel.Adapt<RequiPaymentData>();
        entityCreate.TravelExpensess?.ForEach(c=>c.Id = Guid.NewGuid());
        entityCreate.PaymentPlanContents?.ForEach(c=>c.Id = Guid.NewGuid());
        entityCreate.RequirePaymentMoneys?.ForEach(c=>c.Id = Guid.NewGuid());
        entityCreate.CodeUnit = currentUser.CodeUnit;
        decimal total = 0;
        if (entityCreate.DataContentsList is { Count: > 0 })
        {
            entityCreate.DataContentsList.ForEach(c =>
            {
                total += c.AmountOfMoney;
            });
            entityCreate.TotalMoney = total;
        }
        else if (entityCreate.PaymentPlanContents is { Count: > 0 })
        {
            entityCreate.PaymentPlanContents.ForEach(c =>
            {
                total += (decimal)c.AmountOfMoney;
            });
            entityCreate.TotalMoney = total;
        }
        await dbContext.RequiPaymentData.AddAsync(entityCreate, cancellationToken);
        int rowCount = 0;

        //if (entityCreate.DataContentsList is { Count: > 0 })
        //{
        //    await dbContext.RequiPaymentDataContents.AddRangeAsync(entityCreate.DataContentsList, cancellationToken);
        //}
        //if (entityCreate.HeadInvoiceInputs is { Count: > 0 })
        //{
        //    await dbContext.HeadInvoiceInputs.AddRangeAsync(entityCreate.HeadInvoiceInputs, cancellationToken);
        //}
        //var listHeadInv = await dbContext.HeadInvoiceInputs.Where(c => command.CreateModel.HeadInvoiceIds.Contains(c.Id)).ToListAsync();

        try
        {
            rowCount = await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            foreach (var entry in ex.Entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                foreach (var prop in entry.CurrentValues.Properties)
                {
                    Console.WriteLine($"{prop.Name} = {entry.CurrentValues[prop]}");
                }
            }
            throw;
        }

        if (rowCount <= 0)
        {
            return Result.Failure(new Error("400", "Not ok"));
        }

        await dbContext.HeadInvoiceInputs.Where(c => command.CreateModel.HeadInvoiceIds.Contains(c.Id)).ExecuteUpdateAsync(c =>
                c.SetProperty(a => a.IdRqPayment, entityCreate.Id).SetProperty(a => a.IsPayment, true),
             cancellationToken);

      await  dbContext.HQDTOKHAIMD.Where(c=>c.Id == command.CreateModel.DeclarationId)
          .ExecuteUpdateAsync(c=>c.SetProperty(x=>x.IdRqPayment, entityCreate.Id).SetProperty(x=>x.IsDeclared, true),  cancellationToken);

        if (!command.CreateModel.SaveTemp || command.CreateModel.DataType == "PaymentPlan")
        {
            var cmd = new ConfirmVoucherCommand(new() { Id = entityCreate.Id.ToString(), Parameter = command.CreateModel.DataType!, ConfirmVoucher = 1, Reason = string.Empty, TableName = string.Empty, Status = string.Empty, }, 0);
            await mediator.Send(cmd, cancellationToken);
        }

        Log.Information("User: {0} Created voucher: [{1}] with NumberOfVoucher: {2}", currentUser.CodeUser, command.CreateModel.DataType,
            command.CreateModel.NumberOfVouchers);


        return Result.Success(entityCreate.Id);
    }
}