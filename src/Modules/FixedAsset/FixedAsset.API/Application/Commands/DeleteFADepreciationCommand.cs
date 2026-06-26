namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class DeleteFADepreciationCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<FADepreciationDto> _faDepreciations;

    [DataMember]
    public IEnumerable<FADepreciationDto> FADepreciations => _faDepreciations;


    public DeleteFADepreciationCommand()
    {
        _faDepreciations = [];
    }

    public DeleteFADepreciationCommand(List<FADepreciationDto> faDepreciations)
    {
        _faDepreciations = faDepreciations;
    }
}

