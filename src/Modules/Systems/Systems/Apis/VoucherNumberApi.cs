using Systems.Infrastructure.Entities;

namespace Systems.Apis;

public class VoucherNumberApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("System");

        var api = vApi.MapGroup("voucher-number").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("/get-all", GetAll)
            .WithSummary("Lấy ra danh sách danh mục số chứng từ")
            .WithTags("VoucherNumber");

        api.MapGet("/get-by-id/{id}", GetById)
            .WithSummary("Lấy ra danh mục số chứng từ theo Id")
            .WithTags("VoucherNumber");

        //  api.MapGet("/get-by-coupon-type/{couponType}", GetAll);
        api.MapPost("/create", CreateVoucherNumber)
            .WithSummary("Tạo mới danh mục số chứng từ")
            .WithTags("VoucherNumber");

        api.MapPut("/update", UpdateVoucherNumber)
            .WithSummary("Cập danh mục số chứng từ")
            .WithTags("VoucherNumber");

        api.MapDelete("/delete/{id}", DeleteVoucherNumber)
            .WithSummary("Xóa danh mục số chứng từ")
            .WithTags("VoucherNumber");

    }

    private async Task<IResult> DeleteVoucherNumber(
        [AsParameters] SystemService service,
        Guid id,
        CancellationToken token)
    {
        var data = await service.DbContext.CatalogVoucherNumber
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: token);

        if (data is null)
            return Results.NotFound(Result<bool>.Failure(new Error("404", "Không tìm thấy")));

        service.DbContext.CatalogVoucherNumber.Remove(data);
        await service.DbContext.SaveChangesAsync(token);
        return Results.Ok(Result<bool>.Success(true));
    }

    private async Task<IResult> UpdateVoucherNumber([AsParameters] SystemService service,
        CatalogVoucherNumber request, CancellationToken token)
    {
        var data = await service.DbContext.CatalogVoucherNumber
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: token);

        if (data is null)
            return Results.NotFound(Result<bool>.Failure(new Error("404", "Không tìm thấy")));

        service.Mapper.Map(request, data);
        service.DbContext.CatalogVoucherNumber.Update(data);

        await service.DbContext.SaveChangesAsync(token);

        return Results.Ok(Result<bool>.Success(true));
    }

    private async Task<IResult> CreateVoucherNumber([AsParameters] SystemService service,
        CatalogVoucherNumber request, CancellationToken token)
    {
        service.DbContext.CatalogVoucherNumber.Add(request);
        await service.DbContext.SaveChangesAsync(token);
        return Results.Ok(Result<Guid>.Success(request.Id));

    }

    private async Task<IResult> GetById([AsParameters] SystemService service, Guid id)
    {
        var data = await service.DbContext.CatalogVoucherNumber.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        return data is not null
            ? Results.Ok(Result<CatalogVoucherNumber>.Success(data))
            : Results.NotFound(Result<CatalogVoucherNumber>.Failure(new Error("404", "Không tìm thấy")));
    }



    private async Task<IResult> GetAll([AsParameters] SystemService service)
    {
        var data = await service.DbContext.CatalogVoucherNumber.AsNoTracking().ToListAsync();

        return Results.Ok(Result<List<CatalogVoucherNumber>>.Success(data));
    }
}