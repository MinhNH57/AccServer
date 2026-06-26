using SmartAccCloud.Application.Models.DynamicModel;
using SmartAccCloud.Application.Models.GetDatas;

namespace SmartAccCloud.Application.Services.Dynamic;
public interface IDynamicCreateObjectServices
{
    Task<bool> CreateEntityFromJson(string entityType, string jsonData);
    Task<bool> CreateEntitiesFromJson(string entityType, string jsonData);
    Task<bool> UpdateEntity(UpdateObjectDynamicRequest request);
    Task<bool> UpdateAndRemoveEntity(UpdateAndRemoveObjectDynamicRequest request);
    Task<IEnumerable<object>> GetDynamicEnitity(SmartGetDataQuery request);
    Task<bool> DeleteEntity(SmartDeleteDataQuery request);
    Task<bool> DynamicAddListData(List <DynamicListDataModel> request);
    Task<bool> DynamicAddParentAndChild(DynamicRequestParentAdd request);
}

public class DynamicRequestParentAdd
{
    public CreateObjectDynamicRequest ParentRequest { get; set; } = new();
    public CreateObjectDynamicRequest ContentRequest { get; set; } = new();

}
