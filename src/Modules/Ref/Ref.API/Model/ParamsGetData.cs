using System;

namespace Ref.API.Model;

public class ParamsGetData
{
    public int PSearchType { get; set; }
    public string PSearchValue { get; set; }
    public DateTime PFromDate { get; set; }
    public DateTime PToDate { get; set; }
}