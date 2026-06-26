namespace Systems.Infrastructure.Entities;

public class CatalogRecipientOfMessage
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string? DataType { get; set; }
    public string? DataName { get; set; }
    public string? UserCode { get; set; }
    public string? UserName { get; set; }
    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public string? TokenMessage { get; set; }
    public int? Ordinal { get; set; }
    public bool AllowApprove { get; set; } = true;
    public double? TimeApprove { get; set; }
    public string? OwnerId { get; set; }
    public string? JobCode { get; set; }
    public string? JobName { get; set; }
}

public class ViewRecipientOfMessageByOwner
{
    public Guid Id { get; set; }
    public string? OwnerId { get; set; }
    public string? DataType { get; set; }
    public string? DataName { get; set; }
    public string? JobCode { get; set; }
    public string? JobName { get; set; }
    public string? Users { get; set; }
}