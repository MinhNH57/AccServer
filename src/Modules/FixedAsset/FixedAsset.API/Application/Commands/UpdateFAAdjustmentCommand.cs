namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class UpdateFAAdjustmentCommand
    : IRequest<FAAdjustmentUpdateResponse>
{
    [DataMember]
    private readonly List<FAAdjustmentDetailUpdateRequest> _fAAdjustmentDetails;

    [DataMember]
    private readonly List<FAAdjustmentDetailPostUpdateRequest> _fAAdjustmentDetailPosts;

    [DataMember]
    public FAAdjustmentUpdateRequest FAAdjustment { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FAAdjustmentDetailUpdateRequest> FAAdjustmentDetails => _fAAdjustmentDetails;

    [DataMember]
    public IEnumerable<FAAdjustmentDetailPostUpdateRequest> FAAdjustmentDetailPosts => _fAAdjustmentDetailPosts;

    public UpdateFAAdjustmentCommand()
    {
        _fAAdjustmentDetails = new List<FAAdjustmentDetailUpdateRequest>();
        _fAAdjustmentDetailPosts = new List<FAAdjustmentDetailPostUpdateRequest>();
    }

    public UpdateFAAdjustmentCommand(
        List<FAAdjustmentDetailUpdateRequest> fAAdjustmentDetails,
        List<FAAdjustmentDetailPostUpdateRequest> fAAdjustmentDetailPosts, FAAdjustmentUpdateRequest fAAdjustment,
        AuditingLog auditingLog)
    {
        _fAAdjustmentDetails = fAAdjustmentDetails;
        _fAAdjustmentDetailPosts = fAAdjustmentDetailPosts;
        FAAdjustment = fAAdjustment;
        AuditingLog = auditingLog;
    }
}

