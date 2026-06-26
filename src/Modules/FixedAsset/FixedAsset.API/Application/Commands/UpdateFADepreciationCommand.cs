namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class UpdateFADepreciationCommand
    : IRequest<FADepreciationUpdateResponse>
{
    [DataMember]
    private readonly List<FADepreciationDetailUpdateRequest> _fADepreciationDetails;

    [DataMember]
    private readonly List<FADepreciationDetailAllocationUpdateRequest> _fADepreciationDetailAllocations;

    [DataMember]
    private readonly List<FADepreciationDetailPostUpdateRequest> _fADepreciationDetailPosts;

    [DataMember]
    public FADepreciationUpdateRequest FADepreciation { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FADepreciationDetailUpdateRequest> FADepreciationDetails => _fADepreciationDetails;

    [DataMember]
    public IEnumerable<FADepreciationDetailAllocationUpdateRequest> FADepreciationDetailAllocations => _fADepreciationDetailAllocations;

    [DataMember]
    public IEnumerable<FADepreciationDetailPostUpdateRequest> FADepreciationDetailPosts => _fADepreciationDetailPosts;


    public UpdateFADepreciationCommand()
    {
        _fADepreciationDetails = new List<FADepreciationDetailUpdateRequest>();
        _fADepreciationDetailAllocations = new List<FADepreciationDetailAllocationUpdateRequest>();
        _fADepreciationDetailPosts = new List<FADepreciationDetailPostUpdateRequest>();
    }

    public UpdateFADepreciationCommand(
        List<FADepreciationDetailUpdateRequest> fADepreciationDetails,
        List<FADepreciationDetailAllocationUpdateRequest> fADepreciationDetailAllocations,
        List<FADepreciationDetailPostUpdateRequest> fADepreciationDetailPosts,
        FADepreciationUpdateRequest fADepreciation,
        AuditingLog auditingLog)
    {
        _fADepreciationDetails = fADepreciationDetails;
        _fADepreciationDetailAllocations = fADepreciationDetailAllocations;
        _fADepreciationDetailPosts = fADepreciationDetailPosts;
        FADepreciation = fADepreciation;
        AuditingLog = auditingLog;
    }
}

