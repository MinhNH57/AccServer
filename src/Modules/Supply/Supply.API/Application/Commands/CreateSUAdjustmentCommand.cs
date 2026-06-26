namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class CreateSUAdjustmentCommand
    : IRequest<SUAdjustmentCreateResponse>
{
    [DataMember]
    private readonly List<SUAdjustmentDetailCreateRequest> _suAdjustmentDetails;

    [DataMember]
    private readonly List<SUAdjustmentDetailVoucherCreateRequest> _suAdjustmentDetailVouchers;

    [DataMember]
    public SUAdjustmentCreateRequest SUAdjustment { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUAdjustmentDetailCreateRequest> SUAdjustmentDetails => _suAdjustmentDetails;

    [DataMember]
    public IEnumerable<SUAdjustmentDetailVoucherCreateRequest> SUAdjustmentDetailVouchers => _suAdjustmentDetailVouchers;


    public CreateSUAdjustmentCommand()
    {
        _suAdjustmentDetails = new List<SUAdjustmentDetailCreateRequest>();
        _suAdjustmentDetailVouchers = new List<SUAdjustmentDetailVoucherCreateRequest>();
    }

    public CreateSUAdjustmentCommand(
        List<SUAdjustmentDetailCreateRequest> suAdjustmentDetails,
        List<SUAdjustmentDetailVoucherCreateRequest> suAdjustmentDetailVouchers, SUAdjustmentCreateRequest suAdjustment,
        AuditingLog auditingLog)
    {
        _suAdjustmentDetails = suAdjustmentDetails;
        _suAdjustmentDetailVouchers = suAdjustmentDetailVouchers;
        SUAdjustment = suAdjustment;
        AuditingLog = auditingLog;
    }
}

