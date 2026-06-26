namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class UpdateSUAdjustmentCommand
    : IRequest<SUAdjustmentUpdateResponse>
{
    [DataMember]
    private readonly List<SUAdjustmentDetailUpdateRequest> _suAdjustmentDetails;

    [DataMember]
    private readonly List<SUAdjustmentDetailVoucherUpdateRequest> _suAdjustmentDetailVouchers;

    [DataMember]
    public SUAdjustmentUpdateRequest SUAdjustment { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUAdjustmentDetailUpdateRequest> SUAdjustmentDetails => _suAdjustmentDetails;

    [DataMember]
    public IEnumerable<SUAdjustmentDetailVoucherUpdateRequest> SUAdjustmentDetailVouchers => _suAdjustmentDetailVouchers;

    public UpdateSUAdjustmentCommand()
    {
        _suAdjustmentDetails = new List<SUAdjustmentDetailUpdateRequest>();
        _suAdjustmentDetailVouchers = new List<SUAdjustmentDetailVoucherUpdateRequest>();
    }

    public UpdateSUAdjustmentCommand(
        List<SUAdjustmentDetailUpdateRequest> suAdjustmentDetails,
        List<SUAdjustmentDetailVoucherUpdateRequest> suAdjustmentDetailVouchers, SUAdjustmentUpdateRequest suAdjustment,
        AuditingLog auditingLog)
    {
        _suAdjustmentDetails = suAdjustmentDetails;
        _suAdjustmentDetailVouchers = suAdjustmentDetailVouchers;
        SUAdjustment = suAdjustment;
        AuditingLog = auditingLog;
    }
}

