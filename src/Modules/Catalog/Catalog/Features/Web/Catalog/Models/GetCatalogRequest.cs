namespace Catalog.Features.Web.Catalog.Models;

//public class GetCatalogRequest
//{
//    public string TableName { get; set; } = string.Empty;   
//    public string Condition { get; set; } = string.Empty;
//    public string CodeUser { get; set; } = "ADMIN";
//    public string CodeUnit { get; set; } = "888";
//}
//public record GetCatalogRequest(string TableName, string TypeDocument, string Condition = "", string CodeUser = "ADMIN");

public record SmartGetDataQuery(
    string TableName = "",
    string TypeDocument = "",
    string FirstOrLast = "",
    string Parameter = "Catalog",
    string RequestType = "all",
    string Condition = "",
    string StoreName = "WebSmartGetData"
    );

public record SmartDeleteDataQuery(
    string StoreName,
    string Parameter,
    string TableName,
    string KeyData,
    string DataPlus);

public class DynamicRequestParentAdd
{
    public CreateObjectDynamicRequest ParentRequest { get; set; } = new();
    public CreateObjectDynamicRequest ContentRequest { get; set; } = new();
}
