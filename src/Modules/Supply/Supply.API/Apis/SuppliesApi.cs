using Microsoft.AspNetCore.Http.HttpResults;
using Refit;
using System.Text;
using System.Text.Json;

public static class SuppliesApi
{
    public static RouteGroupBuilder MapSupplyApiV1(this IEndpointRouteBuilder app)
    {
        var routeBuilder = app.NewVersionedApi("Công cụ dụng cụ")
            .RequireAuthorization();

        var api = routeBuilder.MapGroup("api/su/v{apiVersion:apiVersion}")
            .HasApiVersion(1.0);

        api.MapPost("/su_list/supply_ledger", PostSUListSupplyLedgerAsync)
            .WithName("PostSUListSupplyLedger")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Sổ theo dõi CCDC");

        api.MapGet("/organization_unit_get/organization_unit_supply_transfer", GetOrganizationUnitSupplyTransferAsync)
            .WithName("GetOrganizationUnitSupplyTransfer")
            .WithSummary("Lấy đơn vị sử dụng của CCDC")
            .WithDescription("Lấy đơn vị sử dụng của CCDC")
            .WithTags("Danh mục");

        api.MapGet("/su_increment/next_value", GetSUIncrementNextValueAsync)
            .WithName("GetSUIncrementNextValue")
            .WithSummary("Lấy mã CCDC tiếp theo")
            .WithDescription("Lấy mã CCDC tiếp theo")
            .WithTags("Chứng từ");

        api.MapGet("/su_increment/get_su_arising", GetSUIncrementSUArisingAsync)
            .WithName("GetSUIncrementSUArising")
            .WithSummary("Lấy chứng từ phát sinh của CCDC")
            .WithDescription("Lấy chứng từ phát sinh của CCDC")
            .WithTags("Ghi tăng");

        api.MapGet("/su_increment/all_supply_group", GetSUIncrementAllSupplyGroupAsync)
            .WithName("GetSUIncrementAllSupplyGroup")
            .WithSummary("Lấy tất cả nhóm CCDC")
            .WithDescription("Lấy tất cả nhóm CCDC")
            .WithTags("Ghi tăng");

        api.MapGet("/su_increment/detail_full", GetSUIncrementDetailFullAsync)
            .WithName("GetSUIncrementDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Ghi tăng");

        api.MapPost("/su_increment/paging_filter", PostSUIncrementPagingFilterAsync)
            .WithName("PostSUIncrementPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Ghi tăng");

        api.MapPost("/su_increment/save_full", PostSUIncrementSaveFullAsync)
            .WithName("PostSUIncrementSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Ghi tăng");

        api.MapPut("/su_increment/save_full", PutSUIncrementSaveFullAsync)
            .WithName("PutSUIncrementSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Ghi tăng");

        api.MapDelete("/su_increment/batch_voucher", DeleteSUIncrementBatchVoucherAsync)
            .WithName("DeleteSUIncrementBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Ghi tăng");

        api.MapPost("/su_increment/get_data_original", PostSUIncrementGetDataOriginalAsync)
            .WithName("PostSUIncrementGetDataOriginal")
            .WithSummary("Lấy chứng từ nguồn gốc hình thành CCDC")
            .WithDescription("Lấy chứng từ nguồn gốc hình thành CCDC")
            .WithTags("Ghi tăng");

        api.MapGet("/su_increment_get/from_stock", GetSUIncrementGetFromStockAsync)
            .WithName("GetSUIncrementGetFromStock")
            .WithSummary("Lấy chứng từ nguồn gốc hình thành CCDC")
            .WithDescription("Lấy chứng từ nguồn gốc hình thành CCDC")
            .WithTags("Ghi tăng");

        api.MapGet("/su_decrement/detail_full", GetSUDecrementDetailFullAsync)
            .WithName("GetSUDecrementDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Ghi giảm");

        api.MapPost("/su_decrement/paging_filter", PostSUDecrementPagingFilterAsync)
            .WithName("PostSUDecrementPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Ghi giảm");

        api.MapPost("/su_decrement/available_supply_for_decrement", PostSUDecrementAvailableSupplyForDecrementAsync)
            .WithName("PostSUDecrementAvailableSupplyForDecrement")
            .WithSummary("Lấy tất cả CCDC có sẵn")
            .WithDescription("Lấy tất cả CCDC có sẵn")
            .WithTags("Ghi giảm");

        api.MapPost("/su_decrement/save_full", PostSUDecrementSaveFullAsync)
            .WithName("PostSUDecrement")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Ghi giảm");

        api.MapPut("/su_decrement/save_full", PutSUDecrementSaveFullAsync)
            .WithName("PutSUDecrementSaveFullAsync")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Ghi giảm");

        api.MapDelete("/su_decrement/batch_voucher", DeleteSUDecrementBatchVoucherAsync)
            .WithName("DeleteSUDecrementBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Ghi giảm");

        api.MapGet("/su_transfer/detail_full", GetSUTransferDetailFullAsync)
            .WithName("GetSUTransferDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Điều chuyển");

        api.MapPost("/su_transfer/paging_filter", PostSUTransferPagingFilterAsync)
            .WithName("PostSUTransferPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Điều chuyển");

        api.MapGet("/su_transfer/get_supply_transfer", GetSUTransferSupplyTransferAsync)
            .WithName("GetSUTransferSupplyTransfer")
            .WithSummary("Lấy tất cả CCDC điều chuyển")
            .WithDescription("Lấy tất cả CCDC điều chuyển")
            .WithTags("Điều chuyển");

        api.MapPost("/su_transfer/save_full", PostSUTransferSaveFullAsync)
            .WithName("PostSUTransferSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Điều chuyển");

        api.MapPut("/su_transfer/save_full", PutSUTransferSaveFullAsync)
            .WithName("PutSUTransferSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Điều chuyển");

        api.MapDelete("/su_transfer/batch_voucher", DeleteSUTransferAsync)
            .WithName("DeleteSUTransfer")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Điều chuyển");

        api.MapGet("/su_allocation/check_exists_su_allocation", CheckExistsSUAllocationAsync)
            .WithName("CheckExistsSUAllocation")
            .WithSummary("Kiểm tra có phân bổ chi phí CCDC không")
            .WithDescription("Kiểm tra có phân bổ chi phí CCDC không")
            .WithTags("Phân bổ chi phí");

        api.MapPost("/su_allocation/paging_filter", PostSUAllocationPagingFilterAsync)
            .WithName("PostSUAllocationPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Phân bổ chi phí");

        api.MapGet("/su_allocation/su_allocation_expense", GetSUAllocationExpenseAsync)
            .WithName("GetSUAllocationDetail")
            .WithSummary("Lấy chi tiết phân bổ chi phí CCDC")
            .WithDescription("Lấy chi tiết phân bổ chi phí CCDC")
            .WithTags("Phân bổ chi phí");

        api.MapGet("/su_allocation/detail_full", GetSUAllocationDetailFullAsync)
            .WithName("GetSUAllocationDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Phân bổ chi phí");

        api.MapPost("/su_allocation/save_full", PostSUAllocationSaveFullAsync)
            .WithName("PostSUAllocationSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Phân bổ chi phí");

        api.MapPut("/su_allocation/save_full", PutSUAllocationSaveFullAsync)
            .WithName("PutSUAllocationSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Phân bổ chi phí");

        api.MapDelete("/su_allocation/batch_voucher", DeleteSUAllocationAsync)
            .WithName("DeleteSUAllocation")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Phân bổ chi phí");

        api.MapGet("/su_adjustment/detail_full", GetSUAdjustmentDetailFullAsync)
            .WithName("GetSUAdjustmentDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Điều chỉnh");

        api.MapPost("/su_adjustment/paging_filter", PostSUAdjustmentPagingFilterAsync)
            .WithName("PostSUAdjustmentPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Điều chỉnh");

        api.MapPost("/su_adjustment/get_su_adjustment", PostSUAdjustmentGetSUAdjustmentAsync)
            .WithName("PostSUAdjustmentGetSUAdjustment")
            .WithSummary("Lấy tất cả CCDC")
            .WithDescription("Lấy tất cả CCDC")
            .WithTags("Điều chỉnh");

        api.MapPost("/su_adjustment/save_full", PostSUAdjustmentSaveFullAsync)
            .WithName("PostSUAdjustmentSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Điều chỉnh");

        api.MapPut("/su_adjustment/save_full", PutSUAdjustmentSaveFullAsync)
            .WithName("PutSUAdjustmentSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Điều chỉnh");

        api.MapDelete("/su_adjustment/batch_voucher", DeleteSUAdjustmentBatchVoucherAsync)
            .WithName("DeleteSUAdjustment")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Điều chỉnh");

        api.MapGet("/su_audit/detail_full", GetSUAuditDetailFullAsync)
            .WithName("GetSUAuditDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Kiểm kê");

        api.MapPost("/su_audit/paging_filter", PostSUAuditPagingFilterAsync)
            .WithName("PostSUAuditPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Kiểm kê");

        api.MapGet("/su_audit/get_supply_to_audit", GetSUAuditGetSupplyForAuditAsync)
            .WithName("GetSUAuditGetSupplyForAudit")
            .WithSummary("Lấy tất cả tài sản cố định có sẵn")
            .WithDescription("Lấy tất cả tài sản cố định có sẵn")
            .WithTags("Kiểm kê");

        api.MapPost("/su_audit/save_full", PostSUAuditSaveFullAsync)
            .WithName("PostSUAuditSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Kiểm kê");

        api.MapPut("/su_audit/save_full", PutSUAuditSaveFullAsync)
            .WithName("PutSUAuditSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Kiểm kê");

        api.MapDelete("/su_audit/batch_voucher", DeleteSUAuditBatchVoucherAsync)
            .WithName("DeleteSUAuditBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Kiểm kê");

        return api;
    }
    public static async Task<Ok<Response<Pagination<SULedger>>>> PostSUListSupplyLedgerAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetSupplyLedgersAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<SULedger>>()
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<SULedger>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<OrganizationUnitSupplyTransfer>>>> GetOrganizationUnitSupplyTransferAsync(
        [FromQuery(Name = "filter")] string filter,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(filter);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetOrganizationUnitSupplyTransferQuery>(queryString);

            var organizationUnits = await services.Queries.GetOrganizationUnitSupplyTransferAsync(queryParams.SupplyId, queryParams.RefDate);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<OrganizationUnitSupplyTransfer>>()
            {
                Success = true,
                Data = organizationUnits,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<OrganizationUnitSupplyTransfer>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<Dictionary<string, string>>>> GetSUIncrementNextValueAsync(
        [FromQuery(Name = "categories")] int categories,
        [FromQuery(Name = "branch_id")] Guid? branchId,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var data = new Dictionary<string, string>();

            string nextValue = await services.Queries.GetSUIncrementNextValueAsync(categories, branchId);

            if (!string.IsNullOrWhiteSpace(nextValue))
            {
                data.Add(categories.ToString(), nextValue);
            }

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Dictionary<string, string>>()
            {
                Success = true,
                Data = data,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Dictionary<string, string>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<RelatedVoucher>>>> GetSUIncrementSUArisingAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetSupplyArisingQuery>(queryString);
            var relatedVouchers = await services.Queries.GetSUArisingAsync(queryParams.SupplyId, 450, queryParams.DisplayOnBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<RelatedVoucher>>()
            {
                Success = true,
                Data = relatedVouchers,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<RelatedVoucher>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<string>>>> GetSUIncrementAllSupplyGroupAsync(
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var data = new Dictionary<string, string>();

            var supplyGroups = await services.Queries.GetSUIncrementAllSupplyGroupAsync();

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<string>>()
            {
                Success = true,
                Data = supplyGroups,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<string>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<SUIncrementResponse>>> GetSUIncrementDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var suIncrement = await services.Queries.GetSUIncrementDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<SUIncrementResponse>()
            {
                Success = true,
                Data = suIncrement,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUIncrementResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<Pagination<SUIncrementDto>>>> PostSUIncrementPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetSUIncrementsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<SUIncrementDto>>()
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<SUIncrementDto>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUIncrementCreateResponse>>, BadRequest<string>>> PostSUIncrementSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUIncrementCreateRequest suIncrement = JsonSerializer.Deserialize<SUIncrementCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUIncrementDetailAllocationCreateRequest> suIncrementDetailAllocations = [];
                List<SUIncrementDetailSourceCreateRequest> suIncrementDetailSources = [];
                List<SUIncrementDetailCreateRequest> suIncrementDetails = [];
                List<SUIncrementDetailDepartmentCreateRequest> suIncrementDetailDepartments = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_increment_detail_allocation":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailAllocationCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetailAllocations.Add(obj);
                                }
                            }

                            break;
                        case "su_increment_detail_source":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailSourceCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetailSources.Add(obj);
                                }
                            }

                            break;
                        case "su_increment_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetails.Add(obj);
                                }
                            }

                            break;
                        case "su_increment_detail_department":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailDepartmentCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetailDepartments.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUIncrementCommand = new CreateSUIncrementCommand(suIncrementDetailDepartments, suIncrementDetailAllocations, suIncrementDetails, suIncrementDetailSources, suIncrement, auditingLog);

                var requestCreateSUIncrement = new IdentifiedCommand<CreateSUIncrementCommand, SUIncrementCreateResponse>(createSUIncrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateSUIncrement.GetGenericTypeName(),
                    nameof(requestCreateSUIncrement.Id),
                    requestCreateSUIncrement.Id,
                    requestCreateSUIncrement);

                var result = await services.Mediator.Send(requestCreateSUIncrement);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateSupplyCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUIncrementCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateSupplyCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUIncrementCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUIncrementCreateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUIncrementUpdateResponse>>, BadRequest<string>>> PutSUIncrementSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUIncrementUpdateRequest suIncrement = JsonSerializer.Deserialize<SUIncrementUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUIncrementDetailAllocationUpdateRequest> suIncrementDetailAllocations = [];
                List<SUIncrementDetailSourceUpdateRequest> suIncrementDetailSources = [];
                List<SUIncrementDetailUpdateRequest> suIncrementDetails = [];
                List<SUIncrementDetailDepartmentUpdateRequest> suIncrementDetailDepartments = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_increment_detail_allocation":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailAllocationUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetailAllocations.Add(obj);
                                }
                            }

                            break;
                        case "su_increment_detail_source":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailSourceUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetailSources.Add(obj);
                                }
                            }

                            break;
                        case "su_increment_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetails.Add(obj);
                                }
                            }

                            break;
                        case "su_increment_detail_department":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUIncrementDetailDepartmentUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suIncrementDetailDepartments.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var updateSUIncrementCommand = new UpdateSUIncrementCommand(suIncrementDetailDepartments, suIncrementDetailAllocations, suIncrementDetails, suIncrementDetailSources, suIncrement, auditingLog);

                var requestUpdateSUIncrement = new IdentifiedCommand<UpdateSUIncrementCommand, SUIncrementUpdateResponse>(updateSUIncrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateSUIncrement.GetGenericTypeName(),
                    nameof(requestUpdateSUIncrement.Id),
                    requestUpdateSUIncrement.Id,
                    requestUpdateSUIncrement);

                var result = await services.Mediator.Send(requestUpdateSUIncrement);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateSupplyCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUIncrementUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateSupplyCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUIncrementUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUIncrementUpdateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteSUIncrementBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<SUIncrementDto> request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteSUIncrementCommand = new DeleteSUIncrementCommand(request);

                var requestDeleteSUIncrement = new IdentifiedCommand<DeleteSUIncrementCommand, BatchVoucherResponse>(deleteSUIncrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteSUIncrement.GetGenericTypeName(),
                    nameof(requestDeleteSUIncrement.Id),
                    requestDeleteSUIncrement.Id,
                    requestDeleteSUIncrement);

                var result = await services.Mediator.Send(requestDeleteSUIncrement);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteSUIncrementCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteSUIncrementCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Công cụ dụng cụ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<GetDataOriginalResponse>>>> PostSUIncrementGetDataOriginalAsync(
        [FromBody] GetDataOriginalRequest requestBody,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var originals = await services.Queries.GetSUIncrementDataOriginalAsync(
                requestBody.FromDate,
                requestBody.ToDate,
                requestBody.RefType,
                requestBody.ListRefId,
                requestBody.DisplayOnBook,
                requestBody.AccountList,
                requestBody.SupplyId);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<GetDataOriginalResponse>>()
            {
                Success = true,
                Data = originals,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<GetDataOriginalResponse>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<SUIncrementGetDataFromPUStock>>>> GetSUIncrementGetFromStockAsync(
    [FromQuery(Name = "req")] string req,
    [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetDataOriginalRequest>(queryString);
            var stocks = await services.Queries.GetSUIncrementGetFromStockAsync(queryParams.FromDate, queryParams.ToDate, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<SUIncrementGetDataFromPUStock>>()
            {
                Success = true,
                Data = stocks,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<SUIncrementGetDataFromPUStock>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<SUDecrementResponse>>> GetSUDecrementDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var suDecrement = await services.Queries.GetSUDecrementDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<SUDecrementResponse>()
            {
                Success = true,
                Data = suDecrement,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUDecrementResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<Pagination<SUDecrementDto>>>> PostSUDecrementPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetSUDecrementsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<SUDecrementDto>>()
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<SUDecrementDto>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<SUDecrementAvailableSupply>>>> PostSUDecrementAvailableSupplyForDecrementAsync(
        [Body] GetAvailableSupplyForDecrementQuery query,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var availableSupplies = await services.Queries.GetAvailableSupplyForDecrementAsync(query.RefDate);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<SUDecrementAvailableSupply>>()
            {
                Success = true,
                Data = availableSupplies,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<SUDecrementAvailableSupply>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUDecrementCreateResponse>>, BadRequest<string>>> PostSUDecrementSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId, SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUDecrementCreateRequest suDecrement = JsonSerializer.Deserialize<SUDecrementCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUDecrementDetailCreateRequest> suDecrementDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_decrement_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUDecrementDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suDecrementDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUDecrementCommand = new CreateSUDecrementCommand(suDecrementDetails, suDecrement, auditingLog);

                var requestCreateSUDecrement = new IdentifiedCommand<CreateSUDecrementCommand, SUDecrementCreateResponse>(createSUDecrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateSUDecrement.GetGenericTypeName(),
                    nameof(requestCreateSUDecrement.Id),
                    requestCreateSUDecrement.Id,
                    requestCreateSUDecrement);

                var result = await services.Mediator.Send(requestCreateSUDecrement);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateSUDecrementCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUDecrementCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateSUDecrementCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUDecrementCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUDecrementCreateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUDecrementUpdateResponse>>, BadRequest<string>>> PutSUDecrementSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUDecrementUpdateRequest suDecrement = JsonSerializer.Deserialize<SUDecrementUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUDecrementDetailUpdateRequest> suDecrementDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_decrement_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUDecrementDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suDecrementDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUDecrementCommand = new UpdateSUDecrementCommand(suDecrementDetails, suDecrement, auditingLog);

                var requestUpdateSUDecrement = new IdentifiedCommand<UpdateSUDecrementCommand, SUDecrementUpdateResponse>(createSUDecrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateSUDecrement.GetGenericTypeName(),
                    nameof(requestUpdateSUDecrement.Id),
                    requestUpdateSUDecrement.Id,
                    requestUpdateSUDecrement);

                var result = await services.Mediator.Send(requestUpdateSUDecrement);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateSUDecrementCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUDecrementUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateSUDecrementCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUDecrementUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUDecrementUpdateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteSUDecrementBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<SUDecrementDto> request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteSUDecrementCommand = new DeleteSUDecrementCommand(request);

                var requestDeleteSUDecrement = new IdentifiedCommand<DeleteSUDecrementCommand, BatchVoucherResponse>(deleteSUDecrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteSUDecrement.GetGenericTypeName(),
                    nameof(requestDeleteSUDecrement.Id),
                    requestDeleteSUDecrement.Id,
                    requestDeleteSUDecrement);

                var result = await services.Mediator.Send(requestDeleteSUDecrement);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteSUDecrementCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteSUDecrementCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<SUTransferResponse>>> GetSUTransferDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var suTransfer = await services.Queries.GetSUTransferDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<SUTransferResponse>()
            {
                Success = true,
                Data = suTransfer,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUTransferResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<Pagination<SUTransferDto>>>> PostSUTransferPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetSUTransfersAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<SUTransferDto>>()
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<SUTransferDto>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<SupplyTransfer>>>> GetSUTransferSupplyTransferAsync(
            [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var supplyTransfers = await services.Queries.GetSupplyTransfersAsync();

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<SupplyTransfer>>()
            {
                Success = true,
                Data = supplyTransfers,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<SupplyTransfer>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUTransferCreateResponse>>, BadRequest<string>>> PostSUTransferSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUTransferCreateRequest suTransfer = JsonSerializer.Deserialize<SUTransferCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUTransferDetailCreateRequest> suTransferDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_transfer_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUTransferDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suTransferDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }

                var createSUTransferCommand = new CreateSUTransferCommand(suTransferDetails, suTransfer, auditingLog);

                var requestCreateSUTransfer = new IdentifiedCommand<CreateSUTransferCommand, SUTransferCreateResponse>(createSUTransferCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateSUTransfer.GetGenericTypeName(),
                    nameof(requestCreateSUTransfer.Id),
                    requestCreateSUTransfer.Id,
                    requestCreateSUTransfer);

                var result = await services.Mediator.Send(requestCreateSUTransfer);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateSUTransferCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUTransferCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateSUTransferCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUTransferCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUTransferCreateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUTransferUpdateResponse>>, BadRequest<string>>> PutSUTransferSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId, SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUTransferUpdateRequest suTransfer = JsonSerializer.Deserialize<SUTransferUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUTransferDetailUpdateRequest> suTransferDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_transfer_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUTransferDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suTransferDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }

                var createSUTransferCommand = new UpdateSUTransferCommand(suTransferDetails, suTransfer, auditingLog);

                var requestUpdateSUTransfer = new IdentifiedCommand<UpdateSUTransferCommand, SUTransferUpdateResponse>(createSUTransferCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateSUTransfer.GetGenericTypeName(),
                    nameof(requestUpdateSUTransfer.Id),
                    requestUpdateSUTransfer.Id,
                    requestUpdateSUTransfer);

                var result = await services.Mediator.Send(requestUpdateSUTransfer);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateSUTransferCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUTransferUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateSUTransferCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUTransferUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUTransferUpdateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteSUTransferAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<SUTransferDto> request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteSUTransferCommand = new DeleteSUTransferCommand(request);

                var requestDeleteSUTransfer = new IdentifiedCommand<DeleteSUTransferCommand, BatchVoucherResponse>(deleteSUTransferCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteSUTransfer.GetGenericTypeName(),
                    nameof(requestDeleteSUTransfer.Id),
                    requestDeleteSUTransfer.Id,
                    requestDeleteSUTransfer);

                var result = await services.Mediator.Send(requestDeleteSUTransfer);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteSUTransferCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteSUTransferCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<SUAllocationDto>>>> CheckExistsSUAllocationAsync(
        [FromQuery(Name = "req")] string query,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(query);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<CheckExistsSUAllocationQuery>(queryString);

            var suAllocations = await services.Queries.CheckExistsSUAllocationAsync(queryParams.Month, queryParams.Year);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<SUAllocationDto>>()
            {
                Success = true,
                Data = suAllocations,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<SUAllocationDto>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<Pagination<SUAllocationDto>>>> PostSUAllocationPagingFilterAsync(
    [FromBody] PaginationRequest request,
    [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetSUAllocationsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<SUAllocationDto>>()
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<SUAllocationDto>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<SUAllocationExpenseResponse>>> GetSUAllocationExpenseAsync(
        [FromQuery(Name = "req")] string query,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(query);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetSUAllocationExpenseQuery>(queryString);

            var suAllocationExpense = await services.Queries.GetSUAllocationExpenseAsync(queryParams.FromDate, queryParams.ToDate);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<SUAllocationExpenseResponse>()
            {
                Success = true,
                Data = suAllocationExpense,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAllocationExpenseResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<SUAllocationResponse>>> GetSUAllocationDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var suAllocation = await services.Queries.GetSUAllocationDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<SUAllocationResponse>()
            {
                Success = true,
                Data = suAllocation,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAllocationResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUAllocationCreateResponse>>, BadRequest<string>>> PostSUAllocationSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUAllocationCreateRequest suAllocation = JsonSerializer.Deserialize<SUAllocationCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUAllocationDetailExpenseCreateRequest> suAllocationDetailExpenses = [];
                List<SUAllocationDetailTableCreateRequest> suAllocationDetailTables = [];
                List<SUAllocationDetailPostCreateRequest> suAllocationDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_allocation_detail_expense":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAllocationDetailExpenseCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAllocationDetailExpenses.Add(obj);
                                }
                            }

                            break;
                        case "su_allocation_detail_table":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAllocationDetailTableCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAllocationDetailTables.Add(obj);
                                }
                            }

                            break;
                        case "su_allocation_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAllocationDetailPostCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAllocationDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUAllocationCommand = new CreateSUAllocationCommand(suAllocationDetailExpenses, suAllocationDetailTables, suAllocationDetailPosts, suAllocation, auditingLog);

                var requestCreateSUAllocation = new IdentifiedCommand<CreateSUAllocationCommand, SUAllocationCreateResponse>(createSUAllocationCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateSUAllocation.GetGenericTypeName(),
                    nameof(requestCreateSUAllocation.Id),
                    requestCreateSUAllocation.Id,
                    requestCreateSUAllocation);

                var result = await services.Mediator.Send(requestCreateSUAllocation);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateSUAllocationCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUAllocationCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateSUAllocationCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUAllocationCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAllocationCreateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUAllocationUpdateResponse>>, BadRequest<string>>> PutSUAllocationSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUAllocationUpdateRequest suAllocation = JsonSerializer.Deserialize<SUAllocationUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUAllocationDetailExpenseUpdateRequest> suAllocationDetailExpenses = [];
                List<SUAllocationDetailTableUpdateRequest> suAllocationDetailTables = [];
                List<SUAllocationDetailPostUpdateRequest> suAllocationDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_allocation_detail_expense":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAllocationDetailExpenseUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAllocationDetailExpenses.Add(obj);
                                }
                            }

                            break;
                        case "su_allocation_detail_table":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAllocationDetailTableUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAllocationDetailTables.Add(obj);
                                }
                            }

                            break;
                        case "su_allocation_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAllocationDetailPostUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAllocationDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var updateSUAllocationCommand = new UpdateSUAllocationCommand(suAllocationDetailExpenses, suAllocationDetailTables, suAllocationDetailPosts, suAllocation, auditingLog);

                var requestUpdateSUAllocation = new IdentifiedCommand<UpdateSUAllocationCommand, SUAllocationUpdateResponse>(updateSUAllocationCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateSUAllocation.GetGenericTypeName(),
                    nameof(requestUpdateSUAllocation.Id),
                    requestUpdateSUAllocation.Id,
                    requestUpdateSUAllocation);

                var result = await services.Mediator.Send(requestUpdateSUAllocation);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateSUAllocationCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUAllocationUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateSUAllocationCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUAllocationUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAllocationUpdateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteSUAllocationAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<SUAllocationDto> request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteSUAllocationCommand = new DeleteSUAllocationCommand(request);

                var requestDeleteSUAllocation = new IdentifiedCommand<DeleteSUAllocationCommand, BatchVoucherResponse>(deleteSUAllocationCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteSUAllocation.GetGenericTypeName(),
                    nameof(requestDeleteSUAllocation.Id),
                    requestDeleteSUAllocation.Id,
                    requestDeleteSUAllocation);

                var result = await services.Mediator.Send(requestDeleteSUAllocation);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteSUAllocationCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteSUAllocationCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<SUAdjustmentResponse>>> GetSUAdjustmentDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var suAdjustment = await services.Queries.GetSUAdjustmentDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<SUAdjustmentResponse>()
            {
                Success = true,
                Data = suAdjustment,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAdjustmentResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<Pagination<SUAdjustmentDto>>>> PostSUAdjustmentPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetSUAdjustmentsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<SUAdjustmentDto>>()
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<SUAdjustmentDto>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<SUAdjustmentSupplyAvailable>>>> PostSUAdjustmentGetSUAdjustmentAsync(
        [FromBody] GetSUAdjustmentSupplyAvailableQuery query,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var supplyAvailable = await services.Queries.GetSUAdjustmentSupplyAvailableAsync(query.RefDate, query.RefId);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<SUAdjustmentSupplyAvailable>>()
            {
                Success = true,
                Data = supplyAvailable,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<SUAdjustmentSupplyAvailable>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUAdjustmentCreateResponse>>, BadRequest<string>>> PostSUAdjustmentSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId, SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUAdjustmentCreateRequest suAdjustment = JsonSerializer.Deserialize<SUAdjustmentCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUAdjustmentDetailCreateRequest> suAdjustmentDetails = [];
                List<SUAdjustmentDetailVoucherCreateRequest> suAdjustmentDetailVouchers = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_adjustment_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAdjustmentDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAdjustmentDetails.Add(obj);
                                }
                            }

                            break;
                        case "su_adjustment_detail_voucher":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAdjustmentDetailVoucherCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAdjustmentDetailVouchers.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUAdjustmentCommand = new CreateSUAdjustmentCommand(suAdjustmentDetails, suAdjustmentDetailVouchers, suAdjustment, auditingLog);

                var requestCreateSUAdjustment = new IdentifiedCommand<CreateSUAdjustmentCommand, SUAdjustmentCreateResponse>(createSUAdjustmentCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateSUAdjustment.GetGenericTypeName(),
                    nameof(requestCreateSUAdjustment.Id),
                    requestCreateSUAdjustment.Id,
                    requestCreateSUAdjustment);

                var result = await services.Mediator.Send(requestCreateSUAdjustment);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateSUAdjustmentCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUAdjustmentCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateSUAdjustmentCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUAdjustmentCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAdjustmentCreateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }


    public static async Task<Results<Ok<Response<SUAdjustmentUpdateResponse>>, BadRequest<string>>> PutSUAdjustmentSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUAdjustmentUpdateRequest suAdjustment = JsonSerializer.Deserialize<SUAdjustmentUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUAdjustmentDetailUpdateRequest> suAdjustmentDetails = [];
                List<SUAdjustmentDetailVoucherUpdateRequest> suAdjustmentDetailVouchers = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_adjustment_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAdjustmentDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAdjustmentDetails.Add(obj);
                                }
                            }

                            break;
                        case "su_adjustment_detail_voucher":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAdjustmentDetailVoucherUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAdjustmentDetailVouchers.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUAdjustmentCommand = new UpdateSUAdjustmentCommand(suAdjustmentDetails, suAdjustmentDetailVouchers, suAdjustment, auditingLog);

                var requestUpdateSUAdjustment = new IdentifiedCommand<UpdateSUAdjustmentCommand, SUAdjustmentUpdateResponse>(createSUAdjustmentCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateSUAdjustment.GetGenericTypeName(),
                    nameof(requestUpdateSUAdjustment.Id),
                    requestUpdateSUAdjustment.Id,
                    requestUpdateSUAdjustment);

                var result = await services.Mediator.Send(requestUpdateSUAdjustment);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateSUAdjustmentCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUAdjustmentUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateSUAdjustmentCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUAdjustmentUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAdjustmentUpdateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteSUAdjustmentBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<SUAdjustmentDto> request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteSUAdjustmentCommand = new DeleteSUAdjustmentCommand(request);

                var requestDeleteSUAdjustment = new IdentifiedCommand<DeleteSUAdjustmentCommand, BatchVoucherResponse>(deleteSUAdjustmentCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteSUAdjustment.GetGenericTypeName(),
                    nameof(requestDeleteSUAdjustment.Id),
                    requestDeleteSUAdjustment.Id,
                    requestDeleteSUAdjustment);

                var result = await services.Mediator.Send(requestDeleteSUAdjustment);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteSUAdjustmentCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteSUAdjustmentCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }


    public static async Task<Ok<Response<SUAuditResponse>>> GetSUAuditDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var suAudit = await services.Queries.GetSUAuditDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<SUAuditResponse>()
            {
                Success = true,
                Data = suAudit,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAuditResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<Pagination<SUAuditDto>>>> PostSUAuditPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetSUAuditsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<SUAuditDto>>()
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<SUAuditDto>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }


    public static async Task<Ok<Response<List<SUAuditSupplyForAudit>>>> GetSUAuditGetSupplyForAuditAsync(
        [FromQuery(Name = "branch_id")] Guid? branchId,
        [FromQuery(Name = "audit_date")] string auditDate,
        [FromQuery(Name = "is_management_book")] bool isManagementBook,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var availableFixedAssets = await services.Queries.GetSupplyForAuditAsync(branchId, auditDate, isManagementBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<SUAuditSupplyForAudit>>()
            {
                Success = true,
                Data = availableFixedAssets,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<SUAuditSupplyForAudit>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<SUAuditCreateResponse>>, BadRequest<string>>> PostSUAuditSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUAuditCreateRequest suAudit = JsonSerializer.Deserialize<SUAuditCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUAuditDetailCreateRequest> suAuditDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_audit_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAuditDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAuditDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUAuditCommand = new CreateSUAuditCommand(suAuditDetails, suAudit, auditingLog);

                var requestCreateSUAudit = new IdentifiedCommand<CreateSUAuditCommand, SUAuditCreateResponse>(createSUAuditCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateSUAudit.GetGenericTypeName(),
                    nameof(requestCreateSUAudit.Id),
                    requestCreateSUAudit.Id,
                    requestCreateSUAudit);

                var result = await services.Mediator.Send(requestCreateSUAudit);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateSUAuditCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUAuditCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateSUAuditCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUAuditCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAuditCreateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }


    public static async Task<Results<Ok<Response<SUAuditUpdateResponse>>, BadRequest<string>>> PutSUAuditSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                SUAuditUpdateRequest suAudit = JsonSerializer.Deserialize<SUAuditUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<SUAuditDetailUpdateRequest> suAuditDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "su_audit_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<SUAuditDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    suAuditDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createSUAuditCommand = new UpdateSUAuditCommand(suAuditDetails, suAudit, auditingLog);

                var requestUpdateSUAudit = new IdentifiedCommand<UpdateSUAuditCommand, SUAuditUpdateResponse>(createSUAuditCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateSUAudit.GetGenericTypeName(),
                    nameof(requestUpdateSUAudit.Id),
                    requestUpdateSUAudit.Id,
                    requestUpdateSUAudit);

                var result = await services.Mediator.Send(requestUpdateSUAudit);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateSUAuditCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<SUAuditUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateSUAuditCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<SUAuditUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<SUAuditUpdateResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteSUAuditBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<SUAuditDto> request,
        [AsParameters] SupplyServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteSUAuditCommand = new DeleteSUAuditCommand(request);

                var requestDeleteSUAudit = new IdentifiedCommand<DeleteSUAuditCommand, BatchVoucherResponse>(deleteSUAuditCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteSUAudit.GetGenericTypeName(),
                    nameof(requestDeleteSUAudit.Id),
                    requestDeleteSUAudit.Id,
                    requestDeleteSUAudit);

                var result = await services.Mediator.Send(requestDeleteSUAudit);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteSUAuditCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteSUAuditCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return TypedResults.Ok(response);
        }
    }
}