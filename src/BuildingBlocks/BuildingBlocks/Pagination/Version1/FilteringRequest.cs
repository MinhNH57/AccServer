using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Pagination.Version1;

public class FilteringRequest
{
    //[DefaultValue(null)]
    //[FromQuery(Name = "type")]
    //public string Type { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "ids")]
    public string? Ids { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "search")]
    public string? Search { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "filter")]
    public string[]? Filter { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "owner_id")]
    public string? OwnerId { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "fields")]
    public string? Fields { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "status")]
    public string? Status { get; set; } = null;

    //[DefaultValue(null)]
    //[JsonConverter(typeof(DateTimeConverter))]
    //[FromQuery(Name = "created_on_max")]
    //public DateTime? CreatedOnMax { get; set; } = null;

    //[DefaultValue(null)]
    //[JsonConverter(typeof(DateTimeConverter))]
    //[FromQuery(Name = "created_on_min")]
    //public DateTime? CreatedOnMin { get; set; } = null;

    //[DefaultValue(null)]
    //[JsonConverter(typeof(DateTimeConverter))]
    //[FromQuery(Name = "modified_on_max")]
    //public DateTime? ModifiedOnMax { get; set; } = null;

    //[DefaultValue(null)]
    //[JsonConverter(typeof(DateTimeConverter))]
    //[FromQuery(Name = "modified_on_min")]
    //public DateTime? ModifiedOnMin { get; set; } = null;

    public string ToQueryString()
    {
        //var queryString = $"?type={Type}";
        var queryString = string.Empty;
        if (!string.IsNullOrWhiteSpace(Ids)) queryString += $"&ids={Ids}";
        if (!string.IsNullOrWhiteSpace(Search)) queryString += $"&search={Search}";
        if (!string.IsNullOrWhiteSpace(OwnerId)) queryString += $"&owner_id={OwnerId}";
        if (!string.IsNullOrWhiteSpace(Fields)) queryString += $"&fields={Fields}";
        if (!string.IsNullOrWhiteSpace(Status)) queryString += $"&status={Status}";

        return queryString;
    }
}