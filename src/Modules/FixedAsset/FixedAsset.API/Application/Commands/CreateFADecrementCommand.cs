namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class CreateFADecrementCommand
    : IRequest<FADecrementCreateResponse>
{
    [DataMember]
    private readonly List<FADecrementDetailCreateRequest> _fADecrementDetails;

    [DataMember]
    private readonly List<FADecrementDetailPostCreateRequest> _fADecrementDetailPosts;

    [DataMember]
    public FADecrementCreateRequest FADecrement { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FADecrementDetailCreateRequest> FADecrementDetails => _fADecrementDetails;

    [DataMember]
    public IEnumerable<FADecrementDetailPostCreateRequest> FADecrementDetailPosts => _fADecrementDetailPosts;


    public CreateFADecrementCommand()
    {
        _fADecrementDetails = new List<FADecrementDetailCreateRequest>();
        _fADecrementDetailPosts = new List<FADecrementDetailPostCreateRequest>();
    }

    public CreateFADecrementCommand(
        List<FADecrementDetailCreateRequest> fADecrementDetails,
        List<FADecrementDetailPostCreateRequest> fADecrementDetailPosts, FADecrementCreateRequest fADecrement,
        AuditingLog auditingLog)
    {
        _fADecrementDetails = fADecrementDetails;
        _fADecrementDetailPosts = fADecrementDetailPosts;
        FADecrement = fADecrement;
        AuditingLog = auditingLog;
    }
}

