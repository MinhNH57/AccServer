using SmartAccCloud.Application.Models.Catalogs.SurveyExpertise;

namespace SmartAccCloud.Application.Models.Catalogs.Object
{
    public class ObjectViewMobileDetail
    {
        public ObjectDtoFund Object { get; set; } = new();
        public SurveyExpertiseDto SurveyExpertise { get; set; } =  new();
        public List<CatalogRelationshipDto>? LstRelationShip { get; set; }
    }
}
