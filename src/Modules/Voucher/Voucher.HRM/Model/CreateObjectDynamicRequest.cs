using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voucher.HRM.Model;
public class CreateObjectDynamicRequest
{
    public string EntityType { get; set; } = string.Empty;
    public string JsonData { get; set; } = string.Empty;

}

public class UpdateObjectDynamicRequest
{
    public string EntityType { get; set; } = string.Empty;
    public object JsonData { get; set; }

}
public class UpdateAndRemoveObjectDynamicRequest
{
    public string EntityType { get; set; } = string.Empty;
    public string? JsonDataUpdate { get; set; }
    public string? JsonDataCreate { get; set; }

    public List<string>? DataRemove { get; set; } = new();

}


public class DynamicListDataModel
{
    public string EntityType { get; set; } = string.Empty;
    public string JsonDataUpdate { get; set; } = string.Empty;
    public string JsonDataCreate { get; set; } = string.Empty;

    public List<string>? DataRemove { get; set; } = [];

}

public class DynamicListDataBulkModel
{
    public string EntityType { get; set; } = string.Empty;
    public string JsonDataCreate { get; set; } = string.Empty;
}

public class BulkMultiEntityRequest
{
    public List<DynamicListDataBulkModel> Items { get; set; } = new();
}
