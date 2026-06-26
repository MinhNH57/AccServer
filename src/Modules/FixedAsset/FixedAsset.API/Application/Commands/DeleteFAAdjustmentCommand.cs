namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class DeleteFAAdjustmentCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<FAAdjustmentDto> _faAdjustments;

    [DataMember]
    public IEnumerable<FAAdjustmentDto> FAAdjustments => _faAdjustments;


    public DeleteFAAdjustmentCommand()
    {
        _faAdjustments = [];
    }

    public DeleteFAAdjustmentCommand(List<FAAdjustmentDto> faAdjustments)
    {
        _faAdjustments = faAdjustments;
    }
}

