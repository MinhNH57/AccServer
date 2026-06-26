namespace Systems.Infrastructure.Entities;

public class ComponentProperty
{
     public Guid Id { get; set; }

    public int? Number { get; set; }

    public string ComponentCode { get; set; } = "";

    public string FieldCode { get; set; } = "";

    public string FieldName { get; set; } = "";

    public bool Visible { get; set; } = true;

    public bool Freeze { get; set; }

    public string Width { get; set; } = "";

    public string? MinWidth { get; set; } 

    public string? MaxWidth { get; set; } 

    public string? DataType { get; set; } = "string";

    public string Editor { get; set; } = "input";

    public string? TemplateId { get; set; } 

    public string? Placeholder { get; set; }    

    public string? Format { get; set; } 

    public string TextAlign { get; set; } = "text-center";

    public string TextColor { get; set; } = "text-black";

    public string FontStyle { get; set; } = "not-italic";

    public string FontWeight { get; set; } = "font-normal";

    public string BackColor { get; set; } = "bg-inherit";

    public string? CssClass { get; set; } 

    public bool AllowAdding { get; set; }

    public bool AllowEditing { get; set; }

    public bool AllowSearching { get; set; }

    public string SearchOperator { get; set; } = "constains";

    public bool IsGrouping { get; set; }

    public int? GroupOrder { get; set; }

    public bool IsSorting { get; set; }

    public string SortDirection { get; set; } = "ascending";

    public int? SortOrder { get; set; }

    public bool IsAggregate { get; set; }

    public string AggregateType { get; set; } = "count";

    public string? Expression { get; set; } 

    public bool IsParent { get; set; }

    public string? ParentId { get; set; } 

    public string? Status { get; set; } = "active";
    public bool IsPrimaryKey { get; set; }
    public bool ShowInColumnChooser { get; set; } = true;
    public bool IsGroupColumn { get; set; } = false;
    public string? GroupColumnKey { get; set; }  
    public string? GroupColumnName { get; set; }   
    public string? OwnerId { get; set; } = "";

    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; } 

    public DateTime? Modified { get; set; }

    public string? ModifiedBy { get; set; } 
}
