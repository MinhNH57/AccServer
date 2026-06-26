namespace Supply.API.Application.Queries;

public interface ISupplyQueries
{
    Task<List<OrganizationUnitSupplyTransfer>> GetOrganizationUnitSupplyTransferAsync(Guid? supplyId, DateTime refDate);

    Task<string> GetSUIncrementNextValueAsync(int categories, Guid? branchId);

    Task<List<string>> GetSUIncrementAllSupplyGroupAsync();

    Task<Pagination<SULedger>> GetSupplyLedgersAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);
    
    Task<Pagination<SUIncrementDto>> GetSUIncrementsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<SUIncrementResponse> GetSUIncrementDetailFullAsync(string id, int type);

    Task<Pagination<SUDecrementDto>> GetSUDecrementsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<List<RelatedVoucher>> GetSUArisingAsync(Guid refId, int refType, int displayOnBook);

    Task<List<SUDecrementAvailableSupply>> GetAvailableSupplyForDecrementAsync(DateTime refDate);

    Task<List<GetDataOriginalResponse>> GetSUIncrementDataOriginalAsync(DateTime fromDate, DateTime toDate, int refType, List<Guid> listRefId, int displayOnBook, string accountList, Guid? supplyId);
    
    Task<List<SUIncrementGetDataFromPUStock>> GetSUIncrementGetFromStockAsync(DateTime fromDate, DateTime toDate, int refType);

    Task<List<SUAuditSupplyForAudit>> GetSupplyForAuditAsync(Guid? branchId, string auditDate, bool isManagementBook);

    Task<List<SupplyTransfer>> GetSupplyTransfersAsync();

    Task<SUDecrementResponse> GetSUDecrementDetailFullAsync(string id, int type);

    Task<Pagination<SUTransferDto>> GetSUTransfersAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<SUTransferResponse> GetSUTransferDetailFullAsync(string id, int type);

    Task<SUAllocationExpenseResponse> GetSUAllocationExpenseAsync(DateTime fromDate, DateTime toDate);

    Task<List<SUAllocationDto>> CheckExistsSUAllocationAsync(int month, int year);

    Task<SUAllocationResponse> GetSUAllocationDetailFullAsync(string id, int type);

    Task<Pagination<SUAdjustmentDto>> GetSUAdjustmentsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);
    
    Task<Pagination<SUAllocationDto>> GetSUAllocationsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);
    
    Task<Pagination<SUAuditDto>> GetSUAuditsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<SUAdjustmentResponse> GetSUAdjustmentDetailFullAsync(string id, int type);

    Task<SUAuditResponse> GetSUAuditDetailFullAsync(string id, int type);

    Task<List<SUAdjustmentSupplyAvailable>> GetSUAdjustmentSupplyAvailableAsync(DateTime refDate, Guid? refId);

}
