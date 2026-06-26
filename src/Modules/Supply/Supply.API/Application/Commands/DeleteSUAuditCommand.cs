namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class DeleteSUAuditCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<SUAuditDto> _suAudits;

    [DataMember]
    public IEnumerable<SUAuditDto> SUAudits => _suAudits;


    public DeleteSUAuditCommand()
    {
        _suAudits = [];
    }

    public DeleteSUAuditCommand(List<SUAuditDto> suAudits)
    {
        _suAudits = suAudits;
    }
}

