namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class DeleteFATransferCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<FATransferDto> _faTransfers;

    [DataMember]
    public IEnumerable<FATransferDto> FATransfers => _faTransfers;


    public DeleteFATransferCommand()
    {
        _faTransfers = [];
    }

    public DeleteFATransferCommand(List<FATransferDto> faTransfers)
    {
        _faTransfers = faTransfers;
    }
}

