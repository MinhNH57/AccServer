namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class DeleteFAChangeFinancialLeasingToOwnerCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<FAChangeFinancialLeasingToOwnerDto> _fAChangeFinancialLeasingToOwners;

    [DataMember]
    public IEnumerable<FAChangeFinancialLeasingToOwnerDto> FAChangeFinancialLeasingToOwners => _fAChangeFinancialLeasingToOwners;


    public DeleteFAChangeFinancialLeasingToOwnerCommand()
    {
        _fAChangeFinancialLeasingToOwners = [];
    }

    public DeleteFAChangeFinancialLeasingToOwnerCommand(List<FAChangeFinancialLeasingToOwnerDto> fAChangeFinancialLeasingToOwners)
    {
        _fAChangeFinancialLeasingToOwners = fAChangeFinancialLeasingToOwners;
    }
}

