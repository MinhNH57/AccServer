namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class DeleteSUIncrementCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<SUIncrementDto> _suIncrements;

    [DataMember]
    public IEnumerable<SUIncrementDto> SUIncrements => _suIncrements;


    public DeleteSUIncrementCommand()
    {
        _suIncrements = [];
    }

    public DeleteSUIncrementCommand(List<SUIncrementDto> suIncrements)
    {
        _suIncrements = suIncrements;
    }
}

