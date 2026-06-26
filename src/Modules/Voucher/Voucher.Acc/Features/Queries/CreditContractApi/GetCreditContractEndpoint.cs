using BuildingBlocks.Pagination.Version1;
using Carter;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.CreditContractApi;

public class GetCreditContractEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("CreditContract/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("", FindAll)
            .WithName("ListCreditContract")
            .WithSummary("Danh sách phiếu khế ước vay.")
            .WithDescription("Nhận danh sách phiếu được phân trang.")
            .WithTags("CreditContract");

        api.MapGet("{id:Guid}", FindOne)
           .WithName("FindCreditContractContents")
           .WithTags("CreditContract");

        api.MapPut("{id:Guid}", Update)
          .WithName("UpdateCreditContract");
        api.MapDelete("{id:Guid}", Delete)
             .WithTags("CreditContract");
    }

    private static async Task<Results<Ok<ApiResponse<List<CreditContract>>>, BadRequest<ApiResponse<string>>>> FindAll(
        [AsParameters] VoucherServices services,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        CancellationToken token)
    {
        var query = new GetCreditContractQuery(filtering, sorting, pagination);
        var result = await services.Mediator.Send(query, token);
        return TypedResults.Ok(result);
    }
    private static async Task<Ok<ApiResponse<CreditContract>>> FindOne(
   [AsParameters] VoucherServices services,
   [FromRoute] Guid id)
    {
        var creditContract = await services.Context.CreditContract
            .Include(x => x.CreditContractContents.OrderBy(y => y.IdAsc))
            .Include(x=>x.SmartContentsDebtRepaymentPlans.OrderBy(x=>x.PaymentPeriod))
            .Include(x=>x.SmartPaymentVendors)
            .FirstOrDefaultAsync(x => x.Id == id);

        var response = ApiResponseFactory<CreditContract>.Ok(creditContract);

        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<ApiResponse<CreditContract>>, BadRequest<ApiResponse<string>>>> Update(
    [AsParameters] VoucherServices services,
    Guid id,
    CreditContract itemToUpdate)
    {
        try
        {
            var creditContracts = await services.Context.CreditContract
                .Include(x => x.CreditContractContents!.OrderBy(y => y.IdAsc))
                .Include(x => x.SmartContentsDebtRepaymentPlans!.OrderBy(x => x.PaymentPeriod))
                .Include(x=>  x.SmartPaymentVendors)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (creditContracts == null)
            {
                throw new Exception($"Không tìm thấy cấu hình với Id : {id}.");
            }
            else
            {
                TypeAdapterConfig<CreditContract, CreditContract>
                .NewConfig()
                .Ignore(dest => dest.CreditContractContents!)
                .Ignore(dest => dest.SmartContentsDebtRepaymentPlans!)
                 .Ignore(dest => dest.SmartPaymentVendors!);
                services.Mapper.Map(itemToUpdate, creditContracts);
                creditContracts.CreditContractContents = creditContracts.CreditContractContents!
                    .Where(x => itemToUpdate.CreditContractContents!
                        .Select(y => y.IdSource).ToList().Contains(x.IdSource)).ToList();
                creditContracts.ModifyBy = services.CurrentUser.CodeUser;
                creditContracts.ModifyDate = DateTime.Now;
                services.Context.SmartContentsDebtRepaymentPlans.RemoveRange(creditContracts.SmartContentsDebtRepaymentPlans);
                await services.Context.SmartContentsDebtRepaymentPlans.AddRangeAsync(itemToUpdate.SmartContentsDebtRepaymentPlans);
                services.Context.SmartPaymentVendors.RemoveRange(creditContracts.SmartPaymentVendors);
                await services.Context.SmartPaymentVendors.AddRangeAsync(itemToUpdate.SmartPaymentVendors);
                foreach (var creditContractContent in itemToUpdate.CreditContractContents!)
                {
                    var updateChildItem = creditContracts.CreditContractContents.Find(x => x.IdSource == creditContractContent.IdSource);
                    if (updateChildItem != null)
                    {
                        services.Mapper.Map(creditContractContent, updateChildItem);
                    }
                    else
                    {
                        await services.Context.CreditContractContents.AddAsync(creditContractContent);
                    }
                }
            }

            await services.Context.SaveChangesAsync();

            var response = await services.Context.CreditContract
                .Include(x => x.CreditContractContents!.OrderBy(y => y.IdAsc))
                .FirstOrDefaultAsync(x => x.Id == id);

            return TypedResults.Ok(ApiResponseFactory<CreditContract>.Ok(response));

        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }
    private static async Task<Results<Ok<ApiResponse<string>>, BadRequest<ApiResponse<string>>>> Delete(
        [AsParameters] VoucherServices services,
        Guid id)
    {
        try
        {
            var creditContract = await services.Context.CreditContract
                .Include(x => x.CreditContractContents.OrderBy(y => y.IdAsc))
                .Include(x => x.SmartContentsDebtRepaymentPlans.OrderBy(x => x.PaymentPeriod))
                .SingleOrDefaultAsync(i => i.Id == id);

            services.Context.CreditContract.Remove(creditContract);

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(ApiResponseFactory<string>.NoContent());
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }
}