namespace Supply.API.Application.Commands;

[DataContract]
public class CreateSUIncrementCommand
    : IRequest<SUIncrementCreateResponse>
{
    [DataMember]
    private readonly List<SUIncrementDetailDepartmentCreateRequest> _suIncrementDetailDepartments;

    [DataMember]
    private readonly List<SUIncrementDetailAllocationCreateRequest> _suIncrementDetailAllocations;

    [DataMember]
    private readonly List<SUIncrementDetailCreateRequest> _suIncrementDetails;

    [DataMember]
    private readonly List<SUIncrementDetailSourceCreateRequest> _suIncrementDetailSources;

    [DataMember]
    public SUIncrementCreateRequest SUIncrement { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUIncrementDetailDepartmentCreateRequest> SUIncrementDetailDepartments => _suIncrementDetailDepartments;

    [DataMember]
    public IEnumerable<SUIncrementDetailAllocationCreateRequest> SUIncrementDetailAllocations => _suIncrementDetailAllocations;

    [DataMember]
    public IEnumerable<SUIncrementDetailCreateRequest> SUIncrementDetails => _suIncrementDetails;

    [DataMember]
    public IEnumerable<SUIncrementDetailSourceCreateRequest> SUIncrementDetailSources => _suIncrementDetailSources;



    public CreateSUIncrementCommand()
    {
        _suIncrementDetailDepartments = new List<SUIncrementDetailDepartmentCreateRequest>();
        _suIncrementDetailAllocations = new List<SUIncrementDetailAllocationCreateRequest>();
        _suIncrementDetails = new List<SUIncrementDetailCreateRequest>();
        _suIncrementDetailSources = new List<SUIncrementDetailSourceCreateRequest>();
    }

    public CreateSUIncrementCommand(
        List<SUIncrementDetailDepartmentCreateRequest> suIncrementDetailDepartments,
        List<SUIncrementDetailAllocationCreateRequest> suIncrementDetailAllocations,
        List<SUIncrementDetailCreateRequest> suIncrementDetails,
        List<SUIncrementDetailSourceCreateRequest> suIncrementDetailSources,
        SUIncrementCreateRequest suIncrement,
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

