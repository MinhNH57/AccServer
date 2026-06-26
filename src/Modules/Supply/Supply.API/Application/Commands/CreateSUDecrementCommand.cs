namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class CreateSUDecrementCommand
    : IRequest<SUDecrementCreateResponse>
{
    [DataMember]
    private readonly List<SUDecrementDetailCreateRequest> _suDecrementDetails;

    [DataMember]
    public SUDecrementCreateRequest SUDecrement { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUDecrementDetailCreateRequest> SUDecrementDetails => _suDecrementDetails;


    public CreateSUDecrementCommand()
    {
        _suDecrementDetails = new List<SUDecrementDetailCreateRequest>();
    }

    public CreateSUDecrementCommand(
        List<SUDecrementDetailCreateRequest> suDecrementDetails,
        SUDecrementCreateRequest suDecrement,
        AuditingLog auditingLog)
    {
        _suDecrementDetails = suDecrementDetails;
        SUDecrement = suDecrement;
        AuditingLog = auditingLog;
    }
}

