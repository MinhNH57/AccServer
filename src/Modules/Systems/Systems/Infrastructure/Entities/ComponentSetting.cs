namespace Systems.Infrastructure.Entities;

public class ComponentSetting
{
    public Guid Id { get; set; }

    public string ComponentCode { get; set; } = "";

    public string ComponentName { get; set; } = "";

    public string TableName { get; set; } = "";

    public string RequiredField { get; set; } = "";

    public string Height { get; set; } = "100%";

    public string Width { get; set; } = "100%";

    public string? Status { get; set; } = "active";

    public string? OwnerId { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    public string? ModifiedBy { get; set; }
    public string? HelpUrl { get; set; } = string.Empty;
    public string? HelpUrlHtml { get; set; } = string.Empty;
    public int? PageSize { get; set; } = 20;
    public string? PageSizes { get; set; } = "20,50,100";
    public string? ClassGrid { get; set; } = "grid-custom";
    public bool? AllowGroup { get; set; } 
    public bool? EnablePersistence { get; set; } 
    public bool? EnableSum { get; set; } 
    public bool AllowPaging { get; set; } 
    public virtual List<ComponentProperty> ComponentProperties { get; set; } = new();

    public virtual List<ContextMenuProperty> ContextMenuProperties { get; set; } = new();

}
