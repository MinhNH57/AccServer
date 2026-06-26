namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class UpdateFATransferCommand
    : IRequest<FATransferUpdateResponse>
{
    [DataMember]
    private readonly List<FATransferDetailUpdateRequest> _fATransferDetails;

    [DataMember]
    public FATransferUpdateRequest FATransfer { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FATransferDetailUpdateRequest> FATransferDetails => _fATransferDetails;

    public UpdateFATransferCommand()
    {
        _fATransferDetails = new List<FATransferDetailUpdateRequest>();
    }

    public UpdateFATransferCommand(
        List<FATransferDetailUpdateRequest> fATransferDetails,
        FATransferUpdateRequest fATransfer,
        AuditingLog auditingLog)
    {
        _fATransferDetails = fATransferDetails;
        FATransfer = fATransfer;
        AuditingLog = auditingLog;
    }
}

