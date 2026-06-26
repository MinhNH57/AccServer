namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class DeleteFADecrementCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<FADecrementDto> _faDecrements;

    [DataMember]
    public IEnumerable<FADecrementDto> FADecrements => _faDecrements;


    public DeleteFADecrementCommand()
    {
        _faDecrements = [];
    }

    public DeleteFADecrementCommand(List<FADecrementDto> faDecrements)
    {
        _faDecrements = faDecrements;
    }
}

