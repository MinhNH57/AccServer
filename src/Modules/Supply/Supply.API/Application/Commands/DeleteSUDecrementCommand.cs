namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class DeleteSUDecrementCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<SUDecrementDto> _suDecrements;

    [DataMember]
    public IEnumerable<SUDecrementDto> SUDecrements => _suDecrements;


    public DeleteSUDecrementCommand()
    {
        _suDecrements = [];
    }

    public DeleteSUDecrementCommand(List<SUDecrementDto> suDecrements)
    {
        _suDecrements = suDecrements;
    }
}

