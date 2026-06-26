using SmartAccCloud.Application.Models.Catalogs.CatalogDependents;

namespace SmartAccCloud.Application.Services.Catalogs.Dependents;
public interface IDependentsServices
{
    Task<bool> CreateDependents(List<DependentsDto> param);
    Task<bool> EditDependents(List<DependentsDto> param);
    Task<bool> DeleteDependents(List<DependentsDto> param);
}
