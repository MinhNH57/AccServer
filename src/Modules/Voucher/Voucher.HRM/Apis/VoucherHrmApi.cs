using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Pagination.Version1;
using Carter;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Voucher.HRM.Entities;
using Voucher.HRM.Features;
using Voucher.HRM.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Voucher.HRM.Apis;
public class VoucherHrmApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("VoucherHRM");

        var api = vApi.MapGroup("HRMvoucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("{id:guid}", FindOne)
            .WithName("Voucher")
            .WithSummary("Nhận một phiếu.")
            .WithDescription("Nhận một phiếu theo Id.")
            .WithTags("HRMvoucher");

        api.MapPost("", Create)
        .WithName("CreateVoucher")
        .WithSummary("Tạo một phiếu.")
        .WithDescription("Tạo một phiếu mới.")
        .WithTags("HRMvoucher");

        api.MapPut("{id:guid}", Update)
        .WithName("UpdateVoucher")
        .WithSummary("Tạo hoặc thay thế một phiếu.")
        .WithDescription("Tạo hoặc thay thế một phiếu.")
        .WithTags("HRMvoucher");

        api.MapDelete("{id:guid}", Delete)
            .WithName("DeleteVoucher")
            .WithSummary("Xóa phiếu.")
            .WithDescription("Xóa một phiếu chỉ định.")
            .WithTags("Vouchers");

    }

    private static async Task<Ok<ApiResponse<SmartDataApplication>>> FindOne(
        [AsParameters] VoucherService services,
        //[FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] Guid id)
    {
        //var smartData = await services.Context.SmartDatas.FirstOrDefaultAsync(c => c.Id == id);
        var smartData = await services.Context.SmartDataApplication
            .FirstOrDefaultAsync(x => x.Id == id);

        var response = ApiResponseFactory<SmartDataApplication>.Ok(smartData);

        return TypedResults.Ok(response);
    }

    private static async Task<Results<Created<ApiResponse<SmartDataApplication>>, BadRequest<ApiResponse<string>>>> Create(
    [AsParameters] VoucherService services,
    SmartDataApplication itemToCreate)
    {
        try
        {
            if (itemToCreate != null)
            {
                throw new Exception();
            }

            itemToCreate.Id = Guid.NewGuid();

            services.Context.SmartDataApplication.Add(itemToCreate);

            await services.Context.SaveChangesAsync();

            var response = await services.Context.SmartDataApplication
                .FirstOrDefaultAsync(x => x.Id == itemToCreate.Id);

            return TypedResults.Created($"/api/v1/smartData/{itemToCreate.Id}", ApiResponseFactory<SmartDataApplication>.Created(response));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private static async Task<Results<Ok<ApiResponse<SmartDataApplication>>, BadRequest<ApiResponse<string>>>> Update(
    [AsParameters] VoucherService services,
    Guid id,
    SmartDataApplication itemToUpdate)
    {
        try
        {
            var smartData = await services.Context.SmartDataApplication
                .SingleOrDefaultAsync(i => i.Id == id);

            if (smartData == null)
            {
                throw new Exception($"Không tìm thấy cấu hình với Id : {id}.");
            }
            else
            {

                services.Mapper.Map(itemToUpdate, smartData);
            }

            await services.Context.SaveChangesAsync();

            var response = await services.Context.SmartDataApplication
                .FirstOrDefaultAsync(x => x.Id == id);

            return TypedResults.Ok(ApiResponseFactory<SmartDataApplication>.Ok(response));

        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private static async Task<Results<Ok<ApiResponse<string>>, BadRequest<ApiResponse<string>>>> Delete(
    [AsParameters] VoucherService services,
    Guid id)
    {
        try
        {
            var smartData = await services.Context.SmartDataApplication
                .SingleOrDefaultAsync(i => i.Id == id);

            services.Context.SmartDataApplication.Remove(smartData);

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(ApiResponseFactory<string>.NoContent());
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

}
