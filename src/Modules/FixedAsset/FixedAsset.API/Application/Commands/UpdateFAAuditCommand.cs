namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class UpdateFAAuditCommand
    : IRequest<FAAuditUpdateResponse>
{
    [DataMember]
    private readonly List<FAAuditDetailUpdateRequest> _fAAuditDetails;

    [DataMember]
    public FAAuditUpdateRequest FAAudit { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FAAuditDetailUpdateRequest> FAAuditDetails => _fAAuditDetails;

    public UpdateFAAuditCommand()
    {
        _fAAuditDetails = [];
    }

    public UpdateFAAuditCommand(
        List<FAAuditDetailUpdateRequest> fAAuditDetails,
        FAAuditUpdateRequest fAAudit,
        AuditingLog auditingLog)
    {
        _fAAuditDetails = fAAuditDetails;
        FAAudit = fAAudit;
        AuditingLog = auditingLog;
    }
}

