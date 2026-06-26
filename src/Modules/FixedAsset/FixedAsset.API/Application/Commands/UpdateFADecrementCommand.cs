namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class UpdateFADecrementCommand
    : IRequest<FADecrementUpdateResponse>
{
    [DataMember]
    private readonly List<FADecrementDetailUpdateRequest> _fADecrementDetails;

    [DataMember]
    private readonly List<FADecrementDetailPostUpdateRequest> _fADecrementDetailPosts;

    [DataMember]
    public FADecrementUpdateRequest FADecrement { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FADecrementDetailUpdateRequest> FADecrementDetails => _fADecrementDetails;

    [DataMember]
    public IEnumerable<FADecrementDetailPostUpdateRequest> FADecrementDetailPosts => _fADecrementDetailPosts;

    public UpdateFADecrementCommand()
    {
        _fADecrementDetails = new List<FADecrementDetailUpdateRequest>();
        _fADecrementDetailPosts = new List<FADecrementDetailPostUpdateRequest>();
    }

    public UpdateFADecrementCommand(
        List<FADecrementDetailUpdateRequest> fADecrementDetails,
        List<FADecrementDetailPostUpdateRequest> fADecrementDetailPosts, FADecrementUpdateRequest fADecrement,
        AuditingLog auditingLog)
    {
        _fADecrementDetails = fADecrementDetails;
        _fADecrementDetailPosts = fADecrementDetailPosts;
        FADecrement = fADecrement;
        AuditingLog = auditingLog;
    }
}

