using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Pagination.Version1;

public record Filter
{
    [DefaultValue(null)]
    [FromQuery(Name = "field")]
    public string Field { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "operator")]
    public string Operator { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "value")]
    public string Value { get; set; } = null;
}