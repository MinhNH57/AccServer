using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Pagination.Version1;
public class FilterStore
{
    [DefaultValue(null)]
    [FromQuery(Name = "field")]
    public string Field { get; set; } = null;
    [DefaultValue(null)]
    [FromQuery(Name = "value")]
    public string Value { get; set; } = null;

    [DefaultValue(null)]
    [FromQuery(Name = "tableName")]
    public string TableName { get; set; } = null;
     

}
