namespace Catalog.Fund.Models;

public class ObjectViewMobileDetail
{
    public ObjectDtoFund Object { get; set; } = new();
    public SurveyExpertiseDto SurveyExpertise { get; set; } =  new();
    public List<CatalogRelationshipDto>? LstRelationShip { get; set; }
}