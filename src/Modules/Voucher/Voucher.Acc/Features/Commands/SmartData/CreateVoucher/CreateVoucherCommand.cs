using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Voucher.Acc.Features.Commands.ConfirmVoucher;
using Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.SmartData.CreateVoucher;

public class CreateVoucherApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();
        api.MapPost("create-smart-data/", CreateVoucherEndpoint)
            .WithSummary("Tạo mới phiếu");
    }

    private static async Task<IResult> CreateVoucherEndpoint([AsParameters] VoucherServices services, CreateSmartDataModel request, CancellationToken clt)
    {
        var command = new CreateVoucherCommand(request);
        var result = await services.Mediator.Send(command, clt);
        return TypedResults.Ok(result);
    }
}

internal record CreateVoucherCommand(CreateSmartDataModel SmartData) : ICommand<Result>;

internal class CreateVoucherCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser, IMediator mediator) : ICommandHandler<CreateVoucherCommand, Result>
{
    public async Task<Result> Handle(CreateVoucherCommand command, CancellationToken clt)
    {
        var entityCreate = command.SmartData.Adapt<Model.SmartData>();

        decimal totalMoney = 0;
        if (entityCreate.SmartContentsDatas is { Count: > 0 })
        {
            //entityCreate.SmartContentsDatas = new();
            entityCreate.SmartContentsDatas.ForEach(c =>
            {
                c.DataType = entityCreate.DataType;
                totalMoney += c.AmountOfMoney ?? 0;
            });
            entityCreate.TotalMoney = (double)totalMoney;

            //await dbContext.SmartContentsDatas.AddRangeAsync(entityCreate.SmartContentsDatas, clt);
        }
        if (entityCreate.SmartVatTaxLists is { Count: > 0 })
        {
            entityCreate.SmartVatTaxLists.ForEach(c => c.RecordDate = entityCreate.RecordDate);
            //await dbContext.SmartVatTaxList.AddRangeAsync(entityCreate.SmartVatTaxLists, clt);
        }
        if (entityCreate.SmartPaymentVendors is { Count: > 0 })
        {
            entityCreate.SmartPaymentVendors.ForEach(c => c.RecordDate = entityCreate.RecordDate);
            await dbContext.SmartPaymentVendors.AddRangeAsync(entityCreate.SmartPaymentVendors, clt);
        }
        if (entityCreate.SmartFileAttaches is { Count: > 0 })
        {
            //await dbContext.smar.AddRangeAsync(command.SmartData.SmartFileAttaches, clt);
        }

        await dbContext.SmartDatas.AddAsync(entityCreate, clt);
        int rowCount = 0;
        try
        {
            rowCount = await dbContext.SaveChangesAsync(clt);
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

        if (!string.IsNullOrEmpty(entityCreate.InvoiceNumber))
        {
            await dbContext.HeadInvoiceInputs
                .Where(c => c.InvoiceNo == entityCreate.InvoiceNumber)
                .ExecuteUpdateAsync(c =>
                    c.SetProperty(a => a.NumberVouchersDocument, entityCreate.NumberOfVouchers)
                        .SetProperty(a => a.IdDocument, entityCreate.Id),
                    clt);
        }

        Log.Information("User: {0} Created voucher: [{1}] with NumberOfVoucher: {2}", currentUser.CodeUser, command.SmartData.DataType,
        command.SmartData.NumberOfVouchers);

        if (!string.IsNullOrEmpty(command.SmartData.PaymentVoucherNumber))
        {
            int count = await CreatePaymentVoucher(entityCreate, totalMoney, clt);
            if (count <= 0)
            {
                return Result.Success(entityCreate, new Error("1", "Tạo phiếu phụ thất bại"));
            }
        }

        var cmd = new ConfirmVoucherCommand(new() { Id = entityCreate.Id.ToString(), Parameter ="", ConfirmVoucher = 1, Reason = string.Empty, TableName = "SmartData", Status = entityCreate.DataType, }, 0);
        await mediator.Send(cmd, clt);

        return Result.Success(entityCreate.Id);


    }

    private async Task<int> CreatePaymentVoucher(Model.SmartData smartData, decimal total, CancellationToken clt)
    {
        try
        {
            var smartDataPayment = smartData.Adapt<Model.SmartData>();
            //bool sameRef = ReferenceEquals(smartDataPayment, smartData);
            smartDataPayment.SmartContentsDatas = [];
            smartDataPayment.SmartVatTaxLists = null;
            smartDataPayment.SmartFileAttaches = null;

            smartDataPayment.Id = Guid.Empty;
            smartDataPayment.NumberOfVouchers = smartData.PaymentVoucherNumber;
            smartDataPayment.DataType = "CHI";
            smartDataPayment.Description = $"Chi tiền cho {smartData.ObjectName}";
            await dbContext.SmartDatas.AddAsync(smartDataPayment, clt);

            var content = new SmartContentsData
            {
                AmountOfMoney = total,
                CreditSide = "1111",
                DebitObjectCode = smartData.ObjectCode,
                DebitObjectName = smartData.ObjectName,
                DebitObjectTax = smartData.ObjectTaxCode,
                IdContents = smartDataPayment.Id
            };

            smartDataPayment.SmartContentsDatas.Add(content);

            await dbContext.SmartContentsDatas.AddRangeAsync(smartDataPayment.SmartContentsDatas, clt);
        }
        catch (Exception e)
        {
            return -1;
        }

        return await dbContext.SaveChangesAsync(clt);

    }
}