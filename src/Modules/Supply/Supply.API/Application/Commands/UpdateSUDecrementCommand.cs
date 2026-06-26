namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class UpdateSUDecrementCommand
    : IRequest<SUDecrementUpdateResponse>
{
    [DataMember]
    private readonly List<SUDecrementDetailUpdateRequest> _suDecrementDetails;

    [DataMember]
    public SUDecrementUpdateRequest SUDecrement { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUDecrementDetailUpdateRequest> SUDecrementDetails => _suDecrementDetails;

    public UpdateSUDecrementCommand()
    {
        _suDecrementDetails = new List<SUDecrementDetailUpdateRequest>();
    }

    public UpdateSUDecrementCommand(
        List<SUDecrementDetailUpdateRequest> suDecrementDetails,
        SUDecrementUpdateRequest suDecrement,
        AuditingLog auditingLog)
    {
        _suDecrementDetails = suDecrementDetails;
        SUDecrement = suDecrement;
        AuditingLog = auditingLog;
    }
}

