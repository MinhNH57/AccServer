using SmartAccCloud.Application.Models.Catalogs.SurveyExpertise;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper
{
    public class SurveyExpertiseMap : Profile
    {
        public SurveyExpertiseMap()
        {
            CreateMap<SurveyExpertise, SurveyExpertiseDto>().ReverseMap();
        }
    }
}
