using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Pagination.Version1;

public record PaginationRequest
{
    [DefaultValue(null)]
    [FromQuery(Name = "page")]
    public int? Page { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "page_size")]
    public int? PageSize { get; set; } = null;
}