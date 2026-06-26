namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class UpdateFixedAssetCommand
    : IRequest<FAIncrementUpdateResponse>
{
    [DataMember]
    private readonly List<FixedAssetDetailAllocationUpdateRequest> _fixedAssetDetailAllocations;

    [DataMember]
    private readonly List<FixedAssetDetailSourceUpdateRequest> _fixedAssetDetailSources;

    [DataMember]
    private readonly List<FixedAssetDetailUpdateRequest> _fixedAssetDetails;

    [DataMember]
    private readonly List<FixedAssetDetailAccessoryUpdateRequest> _fixedAssetDetailAccessories;

    [DataMember]
    public FixedAssetUpdateRequest FixedAsset { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FixedAssetDetailAllocationUpdateRequest> FixedAssetDetailAllocations => _fixedAssetDetailAllocations;

    [DataMember]
    public IEnumerable<FixedAssetDetailSourceUpdateRequest> FixedAssetDetailSources => _fixedAssetDetailSources;

    [DataMember]
    public IEnumerable<FixedAssetDetailUpdateRequest> FixedAssetDetails => _fixedAssetDetails;

    [DataMember]
    public IEnumerable<FixedAssetDetailAccessoryUpdateRequest> FixedAssetDetailAccessories => _fixedAssetDetailAccessories;


    public UpdateFixedAssetCommand()
    {
        _fixedAssetDetailAllocations = new List<FixedAssetDetailAllocationUpdateRequest>();
        _fixedAssetDetailSources = new List<FixedAssetDetailSourceUpdateRequest>();
        _fixedAssetDetails = new List<FixedAssetDetailUpdateRequest>();
        _fixedAssetDetailAccessories = new List<FixedAssetDetailAccessoryUpdateRequest>();
    }

    public UpdateFixedAssetCommand(List<FixedAssetDetailAllocationUpdateRequest> fixedAssetDetailAllocations, List<FixedAssetDetailSourceUpdateRequest> fixedAssetDetailSources, List<FixedAssetDetailUpdateRequest> fixedAssetDetails, List<FixedAssetDetailAccessoryUpdateRequest> fixedAssetDetailAccessories, FixedAssetUpdateRequest fixedAsset, AuditingLog auditingLog)
    {
        _fixedAssetDetailAllocations = fixedAssetDetailAllocations;
        _fixedAssetDetailSources = fixedAssetDetailSources;
        _fixedAssetDetails = fixedAssetDetails;
        _fixedAssetDetailAccessories = fixedAssetDetailAccessories;
        FixedAsset = fixedAsset;
        AuditingLog = auditingLog;
    }
}

