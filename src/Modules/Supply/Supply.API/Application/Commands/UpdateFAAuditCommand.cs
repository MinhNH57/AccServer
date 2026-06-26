namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class UpdateSUAuditCommand
    : IRequest<SUAuditUpdateResponse>
{
    [DataMember]
    private readonly List<SUAuditDetailUpdateRequest> _suAuditDetails;

    [DataMember]
    public SUAuditUpdateRequest SUAudit { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUAuditDetailUpdateRequest> SUAuditDetails => _suAuditDetails;

    public UpdateSUAuditCommand()
    {
        _suAuditDetails = [];
    }

    public UpdateSUAuditCommand(
        List<SUAuditDetailUpdateRequest> suAuditDetails,
        SUAuditUpdateRequest suAudit,
        AuditingLog auditingLog)
    {
        _suAuditDetails = suAuditDetails;
        SUAudit = suAudit;
        AuditingLog = auditingLog;
    }
}

