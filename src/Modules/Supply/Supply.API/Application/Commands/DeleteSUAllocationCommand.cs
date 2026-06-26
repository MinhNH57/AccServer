namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class DeleteSUAllocationCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<SUAllocationDto> _suAllocations;

    [DataMember]
    public IEnumerable<SUAllocationDto> SUAllocations => _suAllocations;


    public DeleteSUAllocationCommand()
    {
        _suAllocations = [];
    }

    public DeleteSUAllocationCommand(List<SUAllocationDto> suAllocations)
    {
        _suAllocations = suAllocations;
    }
}

