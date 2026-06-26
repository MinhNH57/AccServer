using System.Text.Json.Serialization;
using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Queries.GetDataSynGrid;

public class WhereFilterSyn
{
    [JsonPropertyName("field")]
    public string Field { get; set; }

    /// <summary>Specifies that filter should be incasesensitive.</summary>
    [JsonPropertyName("ignoreCase")]
    public bool IgnoreCase { get; set; }

    /// <summary>
    /// Specifies that ignore accent/diacritic letters while searching.
    /// </summary>
    [JsonPropertyName("ignoreAccent")]
    public bool IgnoreAccent { get; set; }

    /// <summary>
    /// When true it specifies that the filter criteria is a complex one.
    /// </summary>
    [JsonPropertyName("isComplex")]
    public bool IsComplex { get; set; }

    /// <summary>Gets the filter operator.</summary>
    [JsonPropertyName("operator")]
    public string Operator { get; set; }

    /// <summary>Provides the complex filter merge condition.</summary>
    [JsonPropertyName("condition")]
    public string Condition { get; set; }

    /// <summary>Specifies the filter value.</summary>
    [JsonPropertyName("value")]
    public object Value { get; set; }

    /// <summary>Specifies the collection filter criteria.</summary>
    [JsonPropertyName("predicates")]
    public List<WhereFilterSyn> Predicates { get; set; }
}

public class SortSyn
{
    /// <summary>Gets the field name.</summary>
    public string Name { get; set; }

    /// <summary>Gets the sort direction.</summary>
    public string Direction { get; set; }

    /// <summary>Gets the sort comparer</summary>
    public object Comparer { get; set; }
}

public class AggregateSyn
{
    /// <summary>Specifies the field name.</summary>
    [JsonPropertyName("field")]
    public string Field { get; set; }

    /// <summary>Specifies the aggregate type.</summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class SearchFilter
{
    /// <summary>Collection of fields to search.</summary>
    public List<string> Fields { get; set; }

    /// <summary>Specifies the search key.</summary>
    public string Key { get; set; }

    /// <summary>
    /// Specifies the search operator. By default, contains operator will be used.
    /// </summary>
    public string Operator { get; set; }

    /// <summary>Specifies that incasesensitive search to be done.</summary>
    public bool IgnoreCase { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to ignore accent marks and diacritic characters during search operations.
    /// </summary>
    /// <value>
    /// <c>true</c> to treat accented and unaccented characters as equivalent (e.g., "é" as "e"); otherwise, <c>false</c>.
    /// The default is <c>false</c>.
    /// </value>
    /// <remarks>
    /// Enable this option to improve the accuracy and usability of search results in multilingual datasets
    /// where users may omit accent marks. This is especially useful for user-entered queries in globalized applications.
    /// </remarks>
    public bool IgnoreAccent { get; set; }
}


public class GetDataSynGridRequest
{
    /// <summary>Specifies the records to skip.</summary>
    [JsonPropertyName("skip")]
    public int Skip { get; set; }

    /// <summary>Specifies the records to take.</summary>
    [JsonPropertyName("take")]
    public int Take { get; set; }

    [JsonPropertyName("where")]
    public List<WhereFilterSyn>? FilterSyn { get; set; }

    [JsonPropertyName("sorted")]
    public List<SortSyn> SortSyn { get; set; }
    
    [JsonPropertyName("search")]
    public List<SearchFilter> Search { get; set; }
    
    /// <summary>Specifies the aggregate details.</summary>
    [JsonPropertyName("aggregates")]
    public List<AggregateSyn>? Aggregates { get; set; }

    [JsonPropertyName("params")]
    public IDictionary<string, object>? Params { get; set; }
}

internal sealed record GetDataSynGridQuery(GetDataSynGridRequest GetDataSynGridRequest) : IQuery<Result>;