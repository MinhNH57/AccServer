using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Queries.GetVoucherInherit;

internal record GetVoucherInheritQuery(string Parameter, string DataType, string DataTypeInherit, string Id, string BeginDate, string EndDate,string ObjectCode,bool IsTaxAccounting,bool IsAccountsPayable) : IQuery<Result>;