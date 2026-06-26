using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Pagination.Version1;

public record Sort
{
    [DefaultValue(null)]
    [FromQuery(Name = "name")]
    public string Name { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "direction")]
    public string Direction { get; set; } = null;
}