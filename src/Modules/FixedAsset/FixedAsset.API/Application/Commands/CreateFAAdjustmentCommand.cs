namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class CreateFAAdjustmentCommand
    : IRequest<FAAdjustmentCreateResponse>
{
    [DataMember]
    private readonly List<FAAdjustmentDetailCreateRequest> _fAAdjustmentDetails;

    [DataMember]
    private readonly List<FAAdjustmentDetailPostCreateRequest> _fAAdjustmentDetailPosts;

    [DataMember]
    public FAAdjustmentCreateRequest FAAdjustment { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FAAdjustmentDetailCreateRequest> FAAdjustmentDetails => _fAAdjustmentDetails;

    [DataMember]
    public IEnumerable<FAAdjustmentDetailPostCreateRequest> FAAdjustmentDetailPosts => _fAAdjustmentDetailPosts;


    public CreateFAAdjustmentCommand()
    {
        _fAAdjustmentDetails = new List<FAAdjustmentDetailCreateRequest>();
        _fAAdjustmentDetailPosts = new List<FAAdjustmentDetailPostCreateRequest>();
    }

    public CreateFAAdjustmentCommand(
        List<FAAdjustmentDetailCreateRequest> fAAdjustmentDetails,
        List<FAAdjustmentDetailPostCreateRequest> fAAdjustmentDetailPosts, FAAdjustmentCreateRequest fAAdjustment,
        AuditingLog auditingLog)
    {
        _fAAdjustmentDetails = fAAdjustmentDetails;
        _fAAdjustmentDetailPosts = fAAdjustmentDetailPosts;
        FAAdjustment = fAAdjustment;
        AuditingLog = auditingLog;
    }
}

