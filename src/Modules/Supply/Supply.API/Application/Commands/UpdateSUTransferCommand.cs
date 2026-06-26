namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class UpdateSUTransferCommand
    : IRequest<SUTransferUpdateResponse>
{
    [DataMember]
    private readonly List<SUTransferDetailUpdateRequest> _suTransferDetails;

    [DataMember]
    public SUTransferUpdateRequest SUTransfer { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUTransferDetailUpdateRequest> SUTransferDetails => _suTransferDetails;

    public UpdateSUTransferCommand()
    {
        _suTransferDetails = new List<SUTransferDetailUpdateRequest>();
    }

    public UpdateSUTransferCommand(
        List<SUTransferDetailUpdateRequest> suTransferDetails,
        SUTransferUpdateRequest suTransfer,
        AuditingLog auditingLog)
    {
        _suTransferDetails = suTransferDetails;
        SUTransfer = suTransfer;
        AuditingLog = auditingLog;
    }
}

