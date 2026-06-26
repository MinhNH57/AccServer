namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class CreateFAAuditCommand
    : IRequest<FAAuditCreateResponse>
{
    [DataMember]
    private readonly List<FAAuditDetailCreateRequest> _fAAuditDetails;

    [DataMember]
    public FAAuditCreateRequest FAAudit { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FAAuditDetailCreateRequest> FAAuditDetails => _fAAuditDetails;

    public CreateFAAuditCommand()
    {
        _fAAuditDetails = [];
    }

    public CreateFAAuditCommand(
        List<FAAuditDetailCreateRequest> fAAuditDetails,
        FAAuditCreateRequest fAAudit,
        AuditingLog auditingLog)
    {
        _fAAuditDetails = fAAuditDetails;
        FAAudit = fAAudit;
        AuditingLog = auditingLog;
    }
}

