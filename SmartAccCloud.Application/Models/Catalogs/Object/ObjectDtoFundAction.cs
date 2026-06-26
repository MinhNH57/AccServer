using SmartAccCloud.Application.Models.Catalogs.SurveyExpertise;

namespace SmartAccCloud.Application.Models.Catalogs.Object
{
    public class ObjectDtoFundAction
    {
        public ObjectDtoFund Object { get; set; }
        public SurveyExpertiseDto? SurveyExpertise { get; set; }
        public List<CatalogRelationshipDto>? LstRelationShip { get; set; }
    }
}
