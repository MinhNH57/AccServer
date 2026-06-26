namespace Systems.Apis;

public class TenantApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("System");

        var api = vApi.MapGroup("tenants").HasApiVersion(1.0);

        api.MapGet("/get-by-id/{tenantId}", GetTenantById)
            .WithName("get-by-id")
            .WithSummary("Lấy ra thông tin đơn vị thuê")
            .WithDescription("Lấy ra thông tin đơn vị thuê")
            .WithTags("Systems");
    }


    private async Task<IResult> GetTenantById(
        [AsParameters] SystemService service,
        string tenantId)
    {
        var tenant = await service.TenantStore.TryGetByIdentifierAsync(tenantId);
        if (tenant is null)
            return Results.BadRequest(Result.Failure<TenantDto>(new Error("404", "Không tìm thấy đơn vị")));

        var dto = new TenantDto()
        {
            CompanyId = tenant.CompanyId,
            //CompanyAddress = tenant.CompanyAddress,
            ShortName = tenant.ShortName
        };

        return Results.Ok(Result<TenantDto>.Success(dto));
    }
}