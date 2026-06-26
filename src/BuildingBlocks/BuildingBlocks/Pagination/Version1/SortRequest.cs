using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Pagination.Version1;

public record SortRequest
{
    [DefaultValue(null)]
    [FromQuery(Name = "sort")]
    public string[] Sort { get; set; } = null;
}