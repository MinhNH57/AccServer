using System.Text.Json;
using AuditTrait.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace AuditTrait.AuditTrait.Create;

public record LogStoreCommand(Logg1 Log) : ICommand<LogStoreResult>;

public record LogStoreResult(string Name);

public class LogStoreHandle(IDocumentSession session) : ICommandHandler<LogStoreCommand, LogStoreResult>
{
    public async Task<LogStoreResult> Handle(LogStoreCommand request, CancellationToken cancellationToken)
    {
        var datalog = new Datalog()
        {
            Name = "dasjkd",
            Address = "skfjskdfj"
        };
        //request.Log.DataLog = JsonSerializer.Serialize(datalog);
        session.Store(request.Log);
        await session.SaveChangesAsync(cancellationToken);
        return new LogStoreResult("ok");
    }
}