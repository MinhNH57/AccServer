namespace Systems.Infrastructure.Entities;

public class ContextMenuProperty
{
    public Guid Id { get; set; }

    public int? Number { get; set; }

    public string ComponentCode { get; set; } = "";

    public string MenuCode { get; set; } = "";

    public string MenuName { get; set; } = "";

    public bool Visible { get; set; } = true;

    public string MenuIcon { get; set; } = "";

    public string Description { get; set; } = "";

    public bool IsParent { get; set; }

    public int? ParentId { get; set; }

    public string Status { get; set; } = "active";

    public string OwnerId { get; set; } = "";

    public DateTime Created { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = "";

    public DateTime? Modified { get; set; }

    public string ModifiedBy { get; set; } = "";
}
