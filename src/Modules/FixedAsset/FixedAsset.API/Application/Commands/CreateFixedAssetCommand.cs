namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class CreateFixedAssetCommand
    : IRequest<FAIncrementCreateResponse>
{
    [DataMember]
    private readonly List<FixedAssetDetailAllocationCreateRequest> _fixedAssetDetailAllocations;

    [DataMember]
    private readonly List<FixedAssetDetailSourceCreateRequest> _fixedAssetDetailSources;

    [DataMember]
    private readonly List<FixedAssetDetailCreateRequest> _fixedAssetDetails;

    [DataMember]
    private readonly List<FixedAssetDetailAccessoryCreateRequest> _fixedAssetDetailAccessories;

    [DataMember]
    public FixedAssetCreateRequest FixedAsset { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FixedAssetDetailAllocationCreateRequest> FixedAssetDetailAllocations => _fixedAssetDetailAllocations;

    [DataMember]
    public IEnumerable<FixedAssetDetailSourceCreateRequest> FixedAssetDetailSources => _fixedAssetDetailSources;

    [DataMember]
    public IEnumerable<FixedAssetDetailCreateRequest> FixedAssetDetails => _fixedAssetDetails;

    [DataMember]
    public IEnumerable<FixedAssetDetailAccessoryCreateRequest> FixedAssetDetailAccessories => _fixedAssetDetailAccessories;


    public CreateFixedAssetCommand()
    {
        _fixedAssetDetailAllocations = new List<FixedAssetDetailAllocationCreateRequest>();
        _fixedAssetDetailSources = new List<FixedAssetDetailSourceCreateRequest>();
        _fixedAssetDetails = new List<FixedAssetDetailCreateRequest>();
        _fixedAssetDetailAccessories = new List<FixedAssetDetailAccessoryCreateRequest>();
    }

    public CreateFixedAssetCommand(List<FixedAssetDetailAllocationCreateRequest> fixedAssetDetailAllocations, List<FixedAssetDetailSourceCreateRequest> fixedAssetDetailSources, List<FixedAssetDetailCreateRequest> fixedAssetDetails, List<FixedAssetDetailAccessoryCreateRequest> fixedAssetDetailAccessories, FixedAssetCreateRequest fixedAsset, AuditingLog auditingLog)
    {
        _fixedAssetDetailAllocations = fixedAssetDetailAllocations;
        _fixedAssetDetailSources = fixedAssetDetailSources;
        _fixedAssetDetails = fixedAssetDetails;
        _fixedAssetDetailAccessories = fixedAssetDetailAccessories;
        FixedAsset = fixedAsset;
        AuditingLog = auditingLog;
    }
}

