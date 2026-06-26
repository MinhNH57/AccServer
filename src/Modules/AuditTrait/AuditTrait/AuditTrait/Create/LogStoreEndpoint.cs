using System.Text.Json;
using AuditTrait.Models;
using Carter;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuditTrait.AuditTrait.Create;

public class LogStoreEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("auditLog");
        var api = vApi.MapGroup("audit-log").HasApiVersion(1.0);
        //api.RequireAuthorization();

        api.MapPost("/add-log", AddLog);

        api.MapGet("/get", GetAll);
    }

    private async Task<IResult> GetAll([AsParameters] AuditTraitService service)
    {
        var list = await service.DocumentStore.Query<Logg1>().ToListAsync();
        foreach (var item in list)
        {
            if (string.IsNullOrEmpty(item.DataLog)) continue;
            item.DataObj = JsonSerializer.Deserialize<Datalog>(item.DataLog);
        }
        return Results.Ok(list);
    }

    private async Task<Results<Ok, BadRequest<string>>> AddLog([AsParameters] AuditTraitService service,
        LogStoreCommand command)
    {
        var request = await service.Sender.Send(command);
        return TypedResults.Ok();
    }
}