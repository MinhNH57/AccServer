namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class UpdateFAChangeFinancialLeasingToOwnerCommand
    : IRequest<FAChangeFinancialLeasingToOwnerUpdateResponse>
{
    [DataMember]
    private readonly List<FAChangeFinancialLeasingToOwnerDetailUpdateRequest> _fAChangeFinancialLeasingToOwnerDetails;

    [DataMember]
    public FAChangeFinancialLeasingToOwnerUpdateRequest FAChangeFinancialLeasingToOwner { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FAChangeFinancialLeasingToOwnerDetailUpdateRequest> FAChangeFinancialLeasingToOwnerDetails => _fAChangeFinancialLeasingToOwnerDetails;

    public UpdateFAChangeFinancialLeasingToOwnerCommand()
    {
        _fAChangeFinancialLeasingToOwnerDetails = new List<FAChangeFinancialLeasingToOwnerDetailUpdateRequest>();
    }

    public UpdateFAChangeFinancialLeasingToOwnerCommand(
        List<FAChangeFinancialLeasingToOwnerDetailUpdateRequest> fAChangeFinancialLeasingToOwnerDetails,
        FAChangeFinancialLeasingToOwnerUpdateRequest fAChangeFinancialLeasingToOwner,
        AuditingLog auditingLog)
    {
        _fAChangeFinancialLeasingToOwnerDetails = fAChangeFinancialLeasingToOwnerDetails;
        FAChangeFinancialLeasingToOwner = fAChangeFinancialLeasingToOwner;
        AuditingLog = auditingLog;
    }
}

