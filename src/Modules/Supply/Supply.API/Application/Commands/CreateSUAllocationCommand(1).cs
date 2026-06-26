namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class CreateSUAllocationCommand
    : IRequest<SUAllocationCreateResponse>
{
    [DataMember]
    private readonly List<SUAllocationDetailExpenseCreateRequest> _suAllocationDetailExpenses;

    [DataMember]
    private readonly List<SUAllocationDetailTableCreateRequest> _suAllocationDetailTables;

    [DataMember]
    private readonly List<SUAllocationDetailPostCreateRequest> _suAllocationDetailPosts;

    [DataMember]
    public SUAllocationCreateRequest SUAllocation { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUAllocationDetailExpenseCreateRequest> SUAllocationDetailExpenses => _suAllocationDetailExpenses;

    [DataMember]
    public IEnumerable<SUAllocationDetailTableCreateRequest> SUAllocationDetailTables => _suAllocationDetailTables;

    [DataMember]
    public IEnumerable<SUAllocationDetailPostCreateRequest> SUAllocationDetailPosts => _suAllocationDetailPosts;


    public CreateSUAllocationCommand()
    {
        _suAllocationDetailExpenses = new List<SUAllocationDetailExpenseCreateRequest>();
        _suAllocationDetailTables = new List<SUAllocationDetailTableCreateRequest>();
        _suAllocationDetailPosts = new List<SUAllocationDetailPostCreateRequest>();
    }

    public CreateSUAllocationCommand(
        List<SUAllocationDetailExpenseCreateRequest> suAllocationDetailExpenses,
        List<SUAllocationDetailTableCreateRequest> suAllocationDetailTables, 
        List<SUAllocationDetailPostCreateRequest> suAllocationDetailPosts, 
        SUAllocationCreateRequest suAllocation,
        AuditingLog auditingLog)
    {
        _suAllocationDetailExpenses = suAllocationDetailExpenses;
        _suAllocationDetailTables = suAllocationDetailTables;
        _suAllocationDetailPosts = suAllocationDetailPosts;
        SUAllocation = suAllocation;
        AuditingLog = auditingLog;
    }
}

