namespace FixedAsset.API.Application.Models;

public class BatchVoucherResponse
{
    public int TotalProcess { get; set; }
    public int TotalSuccess => TotalProcess - TotalError;
    public int TotalError => MasterErrors.Count;
    public List<MasterError> MasterErrors { get; set; }
}

public class DataError
{
    public Guid RefId { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime RefDate { get; set; }
    public string RefNo { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public string Message { get; set; }
}

public class MasterError
{
    public Guid RefId { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime RefDate { get; set; }
    public string RefNo { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public string Message { get; set; }
    public DataError DataError { get; set; }
}