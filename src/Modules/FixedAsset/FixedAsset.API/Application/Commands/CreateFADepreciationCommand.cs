namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class CreateFADepreciationCommand
    : IRequest<FADepreciationCreateResponse>
{
    [DataMember]
    private readonly List<FADepreciationDetailCreateRequest> _fADepreciationDetails;

    [DataMember]
    private readonly List<FADepreciationDetailAllocationCreateRequest> _fADepreciationDetailAllocations;

    [DataMember]
    private readonly List<FADepreciationDetailPostCreateRequest> _fADepreciationDetailPosts;

    [DataMember]
    public FADepreciationCreateRequest FADepreciation { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FADepreciationDetailCreateRequest> FADepreciationDetails => _fADepreciationDetails;

    [DataMember]
    public IEnumerable<FADepreciationDetailAllocationCreateRequest> FADepreciationDetailAllocations => _fADepreciationDetailAllocations;

    [DataMember]
    public IEnumerable<FADepreciationDetailPostCreateRequest> FADepreciationDetailPosts => _fADepreciationDetailPosts;


    public CreateFADepreciationCommand()
    {
        _fADepreciationDetails = new List<FADepreciationDetailCreateRequest>();
        _fADepreciationDetailAllocations = new List<FADepreciationDetailAllocationCreateRequest>();
        _fADepreciationDetailPosts = new List<FADepreciationDetailPostCreateRequest>();
    }

    public CreateFADepreciationCommand(
        List<FADepreciationDetailCreateRequest> fADepreciationDetails,
        List<FADepreciationDetailAllocationCreateRequest> fADepreciationDetailAllocations, 
        List<FADepreciationDetailPostCreateRequest> fADepreciationDetailPosts, 
        FADepreciationCreateRequest fADepreciation,
        AuditingLog auditingLog)
    {
        _fADepreciationDetails = fADepreciationDetails;
        _fADepreciationDetailAllocations = fADepreciationDetailAllocations;
        _fADepreciationDetailPosts = fADepreciationDetailPosts;
        FADepreciation = fADepreciation;
        AuditingLog = auditingLog;
    }
}

