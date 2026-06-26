using Systems.Infrastructure.Entities;

namespace Systems.Apis;

public class OptionApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("System");

        var api = vApi.MapGroup("option").HasApiVersion(1.0);

        api.MapPost("/", UpdateOptionAsync)
            .WithName("UpdateOption")
            .WithSummary("Cập nhật option")
            .WithDescription("Cập nhật option")
            .WithTags("Systems")
            .RequireAuthorization();
    }

    private async Task<IResult> UpdateOptionAsync(
        [FromBody] List<OptionRequest> request,
        [AsParameters] SystemService service)
    {
        var options = await service.DbContext.SmartOption.ToListAsync();

        foreach (var op in request)
        {
            var option = options.FirstOrDefault(o => o.OpType == op.OptionId);
            if (option != null)
            {
                if (op.ValueType == 2)
                {
                    option.IsActive = bool.Parse(op.OptionValue ?? "false");
                }
                else
                {
                    option.Contenst = op.OptionValue;
                }
            }
            else
            {
                var newOp = new SmartOption()
                {
                    Id = Guid.NewGuid(),
                    CodeUnit = 100,
                    OpType = op.OptionId
                };
                
                if (op.ValueType == 2)
                {
                    newOp.IsActive = bool.Parse(op.OptionValue ?? "false");
                }
                else
                {
                    newOp.Contenst = op.OptionValue;
                }

                await service.DbContext.SmartOption.AddAsync(newOp);
            }
        }

        await service.DbContext.SaveChangesAsync();

        return Results.Ok(true);
    }
}

public class OptionRequest
{
    public string? OptionId { get; set; }
    public int ValueType { get; set; }
    public string? OptionValue { get; set; }
}