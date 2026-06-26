namespace Catalog.Features.Web.Catalog.Models;

public class CatalogSelecttedPrintRequest
{
    public List<string> ValueSelecteds { get; set; } = new();
    public string CatalogTable { get; set; } = string.Empty;
}
