namespace Supply.API.Application.Commands;

using Supply.API.Application.Models;

[DataContract]
public class UpdateSUAllocationCommand
    : IRequest<SUAllocationUpdateResponse>
{
    [DataMember]
    private readonly List<SUAllocationDetailExpenseUpdateRequest> _suAllocationDetailExpenses;

    [DataMember]
    private readonly List<SUAllocationDetailTableUpdateRequest> _suAllocationDetailTables;

    [DataMember]
    private readonly List<SUAllocationDetailPostUpdateRequest> _suAllocationDetailPosts;

    [DataMember]
    public SUAllocationUpdateRequest SUAllocation { get; private set; }

    [DataMember]
    public AuditingLog AuditingLog { get; private set; }

    [DataMember]
    public IEnumerable<SUAllocationDetailExpenseUpdateRequest> SUAllocationDetailExpenses => _suAllocationDetailExpenses;

    [DataMember]
    public IEnumerable<SUAllocationDetailTableUpdateRequest> SUAllocationDetailTables => _suAllocationDetailTables;

    [DataMember]
    public IEnumerable<SUAllocationDetailPostUpdateRequest> SUAllocationDetailPosts => _suAllocationDetailPosts;


    public UpdateSUAllocationCommand()
    {
        _suAllocationDetailExpenses = new List<SUAllocationDetailExpenseUpdateRequest>();
        _suAllocationDetailTables = new List<SUAllocationDetailTableUpdateRequest>();
        _suAllocationDetailPosts = new List<SUAllocationDetailPostUpdateRequest>();
    }

    public UpdateSUAllocationCommand(
        List<SUAllocationDetailExpenseUpdateRequest> suAllocationDetailExpenses,
        List<SUAllocationDetailTableUpdateRequest> suAllocationDetailTables,
        List<SUAllocationDetailPostUpdateRequest> suAllocationDetailPosts,
        SUAllocationUpdateRequest suAllocation,
        AuditingLog auditingLog)
    {
        _suAllocationDetailExpenses = suAllocationDetailExpenses;
        _suAllocationDetailTables = suAllocationDetailTables;
        _suAllocationDetailPosts = suAllocationDetailPosts;
        SUAllocation = suAllocation;
        AuditingLog = auditingLog;
    }
}

