namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class UpdateSUIncrementCommand
    : IRequest<SUIncrementUpdateResponse>
{
    [DataMember]
    private readonly List<SUIncrementDetailDepartmentUpdateRequest> _suIncrementDetailDepartments;

    [DataMember]
    private readonly List<SUIncrementDetailAllocationUpdateRequest> _suIncrementDetailAllocations;

    [DataMember]
    private readonly List<SUIncrementDetailUpdateRequest> _suIncrementDetails;

    [DataMember]
    private readonly List<SUIncrementDetailSourceUpdateRequest> _suIncrementDetailSources;

    [DataMember]
    public SUIncrementUpdateRequest SUIncrement { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUIncrementDetailDepartmentUpdateRequest> SUIncrementDetailDepartments => _suIncrementDetailDepartments;

    [DataMember]
    public IEnumerable<SUIncrementDetailAllocationUpdateRequest> SUIncrementDetailAllocations => _suIncrementDetailAllocations;

    [DataMember]
    public IEnumerable<SUIncrementDetailUpdateRequest> SUIncrementDetails => _suIncrementDetails;

    [DataMember]
    public IEnumerable<SUIncrementDetailSourceUpdateRequest> SUIncrementDetailSources => _suIncrementDetailSources;

    public UpdateSUIncrementCommand()
    {
        _suIncrementDetailDepartments = new List<SUIncrementDetailDepartmentUpdateRequest>();
        _suIncrementDetailAllocations = new List<SUIncrementDetailAllocationUpdateRequest>();
        _suIncrementDetails = new List<SUIncrementDetailUpdateRequest>();
        _suIncrementDetailSources = new List<SUIncrementDetailSourceUpdateRequest>();
    }

    public UpdateSUIncrementCommand(
        List<SUIncrementDetailDepartmentUpdateRequest> suIncrementDetailDepartments,
        List<SUIncrementDetailAllocationUpdateRequest> suIncrementDetailAllocations,
        List<SUIncrementDetailUpdateRequest> suIncrementDetails,
        List<SUIncrementDetailSourceUpdateRequest> suIncrementDetailSources,
        SUIncrementUpdateRequest suIncrement,
        AuditingLog auditingLog)
    {
        _suIncrementDetailDepartments = suIncrementDetailDepartments;
        _suIncrementDetailAllocations = suIncrementDetailAllocations;
        _suIncrementDetails = suIncrementDetails;
        _suIncrementDetailSources = suIncrementDetailSources;
        SUIncrement = suIncrement;
        AuditingLog = auditingLog;
    }
}

