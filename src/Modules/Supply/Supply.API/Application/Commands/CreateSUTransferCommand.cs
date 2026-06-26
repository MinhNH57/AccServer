namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class CreateSUTransferCommand
    : IRequest<SUTransferCreateResponse>
{
    [DataMember]
    private readonly List<SUTransferDetailCreateRequest> _suTransferDetails;

    [DataMember]
    public SUTransferCreateRequest SUTransfer { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUTransferDetailCreateRequest> SUTransferDetails => _suTransferDetails;

    public CreateSUTransferCommand()
    {
        _suTransferDetails = new List<SUTransferDetailCreateRequest>();
    }

    public CreateSUTransferCommand(
        List<SUTransferDetailCreateRequest> suTransferDetails,
        SUTransferCreateRequest suTransfer,
        AuditingLog auditingLog)
    {
        _suTransferDetails = suTransferDetails;
        SUTransfer = suTransfer;
        AuditingLog = auditingLog;
    }
}

