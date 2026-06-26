using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using Carter;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Voucher.Acc.Features.Commands.ConfirmVoucher;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.RequirePayment.Update;

public class RequiPaymentUpdateEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPut("update-require-payment", UpdateVoucher)
            .WithTags("Vouchers");
    }

    private async Task<IResult> UpdateVoucher([AsParameters] VoucherServices services, Guid id, RequiPaymentData request)
    {
        var command = new RequiPaymentUpdateCommand(id, request);
        var result = await services.Mediator.Send(command);

        return TypedResults.Ok(result);
    }
}

internal record RequiPaymentUpdateCommand(Guid Id, RequiPaymentData RequiPaymentData) : ICommand<Result>;

internal class RequiPaymentUpdateCommandHandle(VoucherDbContext dbContext, IMapper mapper, IMediator mediator, IMappingService mappingService) : ICommandHandler<RequiPaymentUpdateCommand, Result>
{
    public async Task<Result> Handle(RequiPaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var requirePayment = await dbContext.RequiPaymentData
            .AsNoTracking()
         .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken: cancellationToken);

        if (requirePayment is null)
            return Result.Failure<bool>(new Error("5", "Not found"));

        var (lstUpdate, lstCreate, lstRemove) = await UpdateContentProcess(command, cancellationToken);
        var (lstInvUpdate, lstInvCreate, lstInvRemove) = await UpdateInvProcess(command, cancellationToken);
        var (lstTravelUpdate, lstTravelCreate, lstTravelRemove) = await UpdateTravelExpensesProcess(command, cancellationToken);
        
        //requirePayment = mapper.Map<RequiPaymentData>(command.RequiPaymentData);

        decimal total = 0;
        if (command.RequiPaymentData.DataContentsList is { Count: > 0 })
        {
            command.RequiPaymentData.DataContentsList.ForEach(c =>
            {
                total += c.AmountOfMoney;
            });
            command.RequiPaymentData.TotalMoney = total;
        }
        else if (command.RequiPaymentData.PaymentPlanContents is { Count: > 0 })
        {
            command.RequiPaymentData.PaymentPlanContents.ForEach(c =>
            {
                total += c.AmountOfMoney;
            });
            command.RequiPaymentData.TotalMoney = total;
        }

        mappingService.MapExistingModels(command.RequiPaymentData, requirePayment);
        dbContext.RequiPaymentData.Update(requirePayment);

        //var entity = dbContext.Entry(requirePayment);
        //entity.CurrentValues.SetValues(command.RequiPaymentData);

        requirePayment.DataContentsList?.Clear();
        requirePayment.HeadInvoiceInputs?.Clear();
        var lstSmartContentDb = mapper.Map<List<RequiPaymentDataContents>>(lstUpdate);
        var lstTravelDb = mapper.Map<List<TravelExpenses>>(lstTravelUpdate);

        var lstInv = mapper.Map<List<HeadInvoiceInputs>>(lstInvUpdate);
        lstCreate.ForEach(c => c.IdContents = command.Id);
        lstTravelCreate.ForEach(c => c.IdContents = command.Id);
        
        await using var trx = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        lstInvCreate.ForEach(c => c.IdRqPayment = command.Id);

        //dbContext.RequiPaymentData.Update(requirePayment);
        await dbContext.RequiPaymentDataContents.AddRangeAsync(lstCreate, cancellationToken);
        dbContext.RequiPaymentDataContents.UpdateRange(lstSmartContentDb);
        dbContext.RequiPaymentDataContents.RemoveRange(lstRemove);

        await dbContext.TravelExpenses.AddRangeAsync(lstTravelCreate, cancellationToken);
        dbContext.TravelExpenses.UpdateRange(lstTravelUpdate);
        dbContext.TravelExpenses.RemoveRange(lstTravelRemove);

        //dbContext.HeadInvoiceInputs.UpdateRange(lstInv);
        //await dbContext.HeadInvoiceInputs.AddRangeAsync(lstInvCreate, cancellationToken);
        //dbContext.HeadInvoiceInputs.RemoveRange(lstInvRemove);
        try
        {
            var rsl = await dbContext.SaveChangesAsync(cancellationToken) > 0;
            //if (rsl && !command.RequiPaymentData.SaveTemp)
            //{
            //    var cmd = new ConfirmVoucherCommand(new() { Id = command.Id.ToString(), Parameter = command.RequiPaymentData.DataType!, ConfirmVoucher = 1, Reason = string.Empty, TableName = string.Empty, Status = string.Empty, }, 0);
            //    await mediator.Send(cmd, cancellationToken);
            //}
            await dbContext.HeadInvoiceInputs.Where(c => lstInvCreate.Select(a => a.Id).Contains(c.Id)).ExecuteUpdateAsync(c =>
                    c.SetProperty(a => a.IdRqPayment, command.Id).SetProperty(a => a.IsPayment, true),
                cancellationToken: cancellationToken);

            await dbContext.HeadInvoiceInputs.Where(c => lstInvRemove.Select(a => a.Id).Contains(c.Id)).ExecuteUpdateAsync(c =>
                    c.SetProperty(a => a.IdRqPayment, Guid.Empty).SetProperty(a => a.IsPayment, false),
                cancellationToken: cancellationToken);

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

        await trx.CommitAsync(cancellationToken);
        if (!command.RequiPaymentData.SaveTemp)
        {
            var cmd = new ConfirmVoucherCommand(new() { Id = command.Id.ToString(), Parameter = command.RequiPaymentData.DataType!, ConfirmVoucher = 1, Reason = string.Empty, TableName = string.Empty, Status = string.Empty, }, 0);
            await mediator.Send(cmd, cancellationToken);
        }
        return Result.Success(true);
    }

    private async Task<(List<RequiPaymentDataContents> lstUpdate, List<RequiPaymentDataContents> lstCreate, List<RequiPaymentDataContents> lstRemove)> UpdateContentProcess(
        RequiPaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var lstSmartContentDb = await dbContext.RequiPaymentDataContents
            .AsNoTracking()
            .Where(c => c.IdContents == command.Id)
            .ToListAsync(cancellationToken);

        if (command.RequiPaymentData.DataContentsList is null or { Count: <= 0 })
        {
            return ([], [], []);
        }

        var lstUpdate = command.RequiPaymentData.DataContentsList
            .Where(c => lstSmartContentDb.Any(r => r.IdSource == c.IdSource))
            .ToList();

        var lstCreate = command.RequiPaymentData.DataContentsList
            .Where(x => lstSmartContentDb.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemove = lstSmartContentDb
            .Where(y => command.RequiPaymentData.DataContentsList.All(x => x.IdSource != y.IdSource))
            .ToList();

        return (lstUpdate, lstCreate, lstRemove);
    }

    private async Task<(List<HeadInvoiceInputs> lstUpdate, List<HeadInvoiceInputs> lstCreate, List<HeadInvoiceInputs> lstRemove)> UpdateInvProcess(
        RequiPaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var lstSmartContentDb = await dbContext.HeadInvoiceInputs
            .AsNoTracking()
            .Where(c => c.IdRqPayment == command.Id)
            .ToListAsync(cancellationToken);

        if (command.RequiPaymentData.HeadInvoiceInputs is null or { Count: <= 0 })
        {
            return ([], [], []);
        }

        var lstUpdate = command.RequiPaymentData.HeadInvoiceInputs
            .Where(c => lstSmartContentDb.Any(r => r.Id == c.Id))
            .ToList();

        var lstCreate = command.RequiPaymentData.HeadInvoiceInputs
            .Where(x => lstSmartContentDb.All(y => y.Id != x.Id))
            .ToList();

        var lstRemove = lstSmartContentDb
            .Where(y => command.RequiPaymentData.HeadInvoiceInputs.All(x => x.Id != y.Id))
            .ToList();

        return (lstUpdate, lstCreate, lstRemove);
    }

    private async Task<(List<TravelExpenses> lstUpdate, List<TravelExpenses> lstCreate, List<TravelExpenses> lstRemove)> UpdateTravelExpensesProcess(
        RequiPaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var lstSmartContentDb = await dbContext.TravelExpenses
            .AsNoTracking()
            .Where(c => c.IdContents == command.Id)
            .ToListAsync(cancellationToken);

        if (command.RequiPaymentData.TravelExpensess is null or { Count: <= 0 })
        {
            return ([], [], []);
        }

        var lstUpdate = command.RequiPaymentData.TravelExpensess
            .Where(c => lstSmartContentDb.Any(r => r.Id == c.Id))
            .ToList();

        var lstCreate = command.RequiPaymentData.TravelExpensess
            .Where(x => lstSmartContentDb.All(y => y.Id != x.Id))
            .ToList();

        var lstRemove = lstSmartContentDb
            .Where(y => command.RequiPaymentData.TravelExpensess.All(x => x.Id != y.Id))
            .ToList();

        return (lstUpdate, lstCreate, lstRemove);
    }
}