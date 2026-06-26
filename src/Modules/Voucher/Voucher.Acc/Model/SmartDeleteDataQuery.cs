namespace Voucher.Acc.Model;

public record SmartDeleteDataQuery(
    string StoreName,
    string Parameter,
    string TableName,
    string KeyData,
    string DataPlus);