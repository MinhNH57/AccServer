using SmartAccCloud.Application.Models.Catalogs.CatalogSalaryInsuranceRegulations;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogSalaryInsuranceRegulationsMap: Profile
{
    public CatalogSalaryInsuranceRegulationsMap()
    {
        CreateMap<CatalogSalaryInsuranceRegulations, CatalogSalaryInsuranceRegulationsDto>().ReverseMap();
        CreateMap<CatalogSalaryInsuranceRegulations, CatalogSalaryInsuranceVm>().ReverseMap();
    }
}
