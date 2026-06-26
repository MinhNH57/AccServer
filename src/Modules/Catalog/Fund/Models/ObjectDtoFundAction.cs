namespace Catalog.Fund.Models;

public class ObjectDtoFundAction 
{
    public ObjectDtoFund Object { get; set; }
    public SurveyExpertiseDto? SurveyExpertise { get; set; }
    public List<CatalogRelationshipDto>? LstRelationShip { get; set; }
}