namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class DeleteFAAuditCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<FAAuditDto> _faAudits;

    [DataMember]
    public IEnumerable<FAAuditDto> FAAudits => _faAudits;


    public DeleteFAAuditCommand()
    {
        _faAudits = [];
    }

    public DeleteFAAuditCommand(List<FAAuditDto> faAudits)
    {
        _faAudits = faAudits;
    }
}

