namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class CreateFAChangeFinancialLeasingToOwnerCommand
    : IRequest<FAChangeFinancialLeasingToOwnerCreateResponse>
{
    [DataMember]
    private readonly List<FAChangeFinancialLeasingToOwnerDetailCreateRequest> _fAChangeFinancialLeasingToOwnerDetails;

    [DataMember]
    public FAChangeFinancialLeasingToOwnerCreateRequest FAChangeFinancialLeasingToOwner { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<FAChangeFinancialLeasingToOwnerDetailCreateRequest> FAChangeFinancialLeasingToOwnerDetails => _fAChangeFinancialLeasingToOwnerDetails;

    public CreateFAChangeFinancialLeasingToOwnerCommand()
    {
        _fAChangeFinancialLeasingToOwnerDetails = new List<FAChangeFinancialLeasingToOwnerDetailCreateRequest>();
    }

    public CreateFAChangeFinancialLeasingToOwnerCommand(
        List<FAChangeFinancialLeasingToOwnerDetailCreateRequest> fAChangeFinancialLeasingToOwnerDetails,
        FAChangeFinancialLeasingToOwnerCreateRequest fAChangeFinancialLeasingToOwner,
        AuditingLog auditingLog)
    {
        _fAChangeFinancialLeasingToOwnerDetails = fAChangeFinancialLeasingToOwnerDetails;
        FAChangeFinancialLeasingToOwner = fAChangeFinancialLeasingToOwner;
        AuditingLog = auditingLog;
    }
}

