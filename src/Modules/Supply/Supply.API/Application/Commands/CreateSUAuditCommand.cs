namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class CreateSUAuditCommand
    : IRequest<SUAuditCreateResponse>
{
    [DataMember]
    private readonly List<SUAuditDetailCreateRequest> _suAuditDetails;

    [DataMember]
    public SUAuditCreateRequest SUAudit { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUAuditDetailCreateRequest> SUAuditDetails => _suAuditDetails;

    public CreateSUAuditCommand()
    {
        _suAuditDetails = [];
    }

    public CreateSUAuditCommand(
        List<SUAuditDetailCreateRequest> suAuditDetails,
        SUAuditCreateRequest suAudit,
        AuditingLog auditingLog)
    {
        _suAuditDetails = suAuditDetails;
        SUAudit = suAudit;
        AuditingLog = auditingLog;
    }
}

