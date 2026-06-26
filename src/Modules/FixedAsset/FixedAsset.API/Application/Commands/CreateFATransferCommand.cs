namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class CreateFATransferCommand
    : IRequest<FATransferCreateResponse>
{
    [DataMember]
    private readonly List<FATransferDetailCreateRequest> _fATransferDetails;

    [DataMember]
    public FATransferCreateRequest FATransfer { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FATransferDetailCreateRequest> FATransferDetails => _fATransferDetails;

    public CreateFATransferCommand()
    {
        _fATransferDetails = new List<FATransferDetailCreateRequest>();
    }

    public CreateFATransferCommand(
        List<FATransferDetailCreateRequest> fATransferDetails,
        FATransferCreateRequest fATransfer,
        AuditingLog auditingLog)
    {
        _fATransferDetails = fATransferDetails;
        FATransfer = fATransfer;
        AuditingLog = auditingLog;
    }
}

