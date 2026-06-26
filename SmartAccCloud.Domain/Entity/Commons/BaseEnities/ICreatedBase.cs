namespace SmartAccCloud.Domain.Entity.Commons.BaseEnities;

public interface ICreatedBase
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
}