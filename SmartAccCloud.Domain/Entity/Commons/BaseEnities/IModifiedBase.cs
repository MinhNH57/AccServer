namespace SmartAccCloud.Domain.Entity.Commons.BaseEnities;

public interface IModifiedBase
{
    public DateTime LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}