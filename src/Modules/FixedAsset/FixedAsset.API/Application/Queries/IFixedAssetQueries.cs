namespace FixedAsset.API.Application.Queries;

public interface IFixedAssetQueries
{
    Task<string> GetFixedAssetNextValueAsync(int categories, Guid? branchId);

    Task<Pagination<FixedAssetDto>> GetFixedAssetsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<FixedAssetResponse> GetFixedAssetDetailFullAsync(string id, int type);

    Task<Pagination<FADecrementDto>> GetFADecrementsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<List<FADecrementAvailableFixedAsset>> GetFADecrementAvailableFixedAssetsAsync(DateTime postedDate, Guid? branchID = null, bool isManagementBook = false);
    
    Task<List<GetDataOriginalResponse>> GetFADecrementSUOriginalFADecrementAsync(DateTime fromDate, DateTime toDate, int refType, List<Guid> listRefId);
    
    Task<List<SelectSUFromFADecrement>> GetFADecrementGetSelectSUFromFADecrementAsync(DateTime fromDate, DateTime toDate);

    Task<List<FATransferAvailableFixedAsset>> GetFATransferAvailableFixedAssetsAsync(DateTime postedDate, Guid? branchID = null, bool isManagementBook = false);

    Task<List<FAChangeFinancialLeasingToOwnerAvailableFixedAsset>> GetFAChangeFinancialLeasingToOwnerAvailableFixedAssetsAsync(DateTime toDate, Guid? fixedAssetId);

    Task<List<FADecrementCustomFixedAsset>> GetFADecrementCustomFixedAssetsAsync(string fixedAssetIDs, Guid? branchID = null, DateTime? postedDate = null, bool isManagementBook = false);

    Task<FADecrementResponse> GetFADecrementDetailFullAsync(string id, int type);

    Task<Pagination<FATransferDto>> GetFATransfersAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<FATransferResponse> GetFATransferDetailFullAsync(string id, int type);

    Task<FADepreciationDetailResponse> GetFADepreciationDetailAsync(DateTime postedDate);

    Task<Pagination<FADepreciationDto>> GetFADepreciationAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<List<FADepreciationDto>> CheckExistsFADepreciationAsync(int month, int year);

    Task<List<FAAuditCheckExistFADecrementToFAAudit>> CheckExistFADecrementToFAAuditAsync(string ids);

    Task<FADepreciationResponse> GetFADepreciationDetailFullAsync(string id, int type);

    Task<Pagination<FAAdjustmentDto>> GetFAAdjustmentsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<Pagination<FAAuditDto>> GetFAAuditsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);
    
    Task<Pagination<FAChangeFinancialLeasingToOwnerDto>> GetFAChangeFinancialLeasingToOwnersAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode);

    Task<FAChangeFinancialLeasingToOwnerResponse> GetFAChangeFinancialLeasingToOwnerDetailFullAsync(string id, int type);

    Task<List<RelatedVoucher>> GetFAArisingAsync(Guid refId, int refType, int displayOnBook);

    Task<FAAdjustmentResponse> GetFAAdjustmentDetailFullAsync(string id, int type);

    Task<FAAuditResponse> GetFAAuditDetailFullAsync(string id, int type);

    Task<List<FAAdjustmentAvailableFixedAsset>> GetFAAdjustmentAvailableFixedAssetsAsync(DateTime postedDate, Guid? branchID = null, bool isManagementBook = false);

    Task<List<FAAuditAvailableFixedAsset>> GetFAAuditAvailableFixedAssetsAsync(DateTime auditDate);

    Task<List<FAAdjustmentCustomFixedAsset>> GetFAAdjustmentCustomFixedAssetsAsync(string fixedAssetIDs, Guid? branchID = null, DateTime? postedDate = null, bool isManagementBook = false);

}
