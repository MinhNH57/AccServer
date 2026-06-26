namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class DeleteSUTransferCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<SUTransferDto> _suTransfers;

    [DataMember]
    public IEnumerable<SUTransferDto> SUTransfers => _suTransfers;


    public DeleteSUTransferCommand()
    {
        _suTransfers = [];
    }

    public DeleteSUTransferCommand(List<SUTransferDto> suTransfers)
    {
        _suTransfers = suTransfers;
    }
}

