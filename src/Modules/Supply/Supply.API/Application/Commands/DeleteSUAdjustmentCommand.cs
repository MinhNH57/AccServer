namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class DeleteSUAdjustmentCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<SUAdjustmentDto> _suAdjustments;

    [DataMember]
    public IEnumerable<SUAdjustmentDto> SUAdjustments => _suAdjustments;


    public DeleteSUAdjustmentCommand()
    {
        _suAdjustments = [];
    }

    public DeleteSUAdjustmentCommand(List<SUAdjustmentDto> suAdjustments)
    {
        _suAdjustments = suAdjustments;
    }
}

