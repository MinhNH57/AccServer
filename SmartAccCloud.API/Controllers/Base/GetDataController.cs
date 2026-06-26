using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Models.DynamicModel;
using SmartAccCloud.Application.Models.GetDatas;
using SmartAccCloud.Application.Services.Dynamic;
using SmartAccCloud.Domain;

namespace SmartAccCloud.API.Controllers.Base;
[Route("api/[controller]")]
[ApiController]
public class GetDataController(
    IApplicationDbContext context,
    IDynamicCreateObjectServices dynamicCreateObjectServices) : ResultControllerBase
{

    [HttpPost]
    [Route("get-catalog")]
    //[Authorize]
    public async Task<IActionResult> GetCatalog(SmartGetDataQuery request)
    {
        var data = await dynamicCreateObjectServices.GetDynamicEnitity(request);
        return Ok(data);
    }

    [HttpGet]
    [Route("get-catalog")]
    //[Authorize]
    public async Task<IActionResult> GetCatalogMoblie([FromQuery] SmartGetDataQuery request)
    {
        var data = await dynamicCreateObjectServices.GetDynamicEnitity(request);
        return Ok(data);
    }

    [HttpPost]
    [Route("delete-catalog")]
    [Authorize]
    public async Task<IActionResult> DeleteCatalog(SmartDeleteDataQuery request)
    {
        var result = await dynamicCreateObjectServices.DeleteEntity(request);

        return Ok(result);
    }

    [HttpPost]
    [Route("dynamic-create-object")]
    [Authorize]
    public async Task<IActionResult> CreateDynamicObject(CreateObjectDynamicRequest request)
    {
        var success = await dynamicCreateObjectServices.CreateEntityFromJson(request.EntityType, request.JsonData);
        if (success)
        {
            return Ok("Entity created successfully.");
        }

        return BadRequest("Failed to create entity.");
    }

    [HttpPost]
    [Route("dynamic-create-list-object")]
    [Authorize]
    public async Task<IActionResult> CreateDynamicObjectList(CreateObjectDynamicRequest request)
    {
        var success = await dynamicCreateObjectServices.CreateEntitiesFromJson(request.EntityType, request.JsonData);
        if (success)
        {
            return Ok("Entity created successfully.");
        }

        return BadRequest("Failed to create entity.");
    }

    [HttpPut]
    [Route("dynamic-update-object")]
    [Authorize]
    public async Task<IActionResult> UpdateDynamicObject(UpdateObjectDynamicRequest request)
    {
        var success = await dynamicCreateObjectServices.UpdateEntity(request);
        if (success)
        {
            return Ok("Entity created successfully.");
        }
        return BadRequest("Failed to create entity.");
    }
    [HttpPut]
    [Route("dynamic-update-and-remove-object")]
    [Authorize]
    public async Task<IActionResult> UpdateAndRemoveDynamicObject(UpdateAndRemoveObjectDynamicRequest request)
    {
        var success = await dynamicCreateObjectServices.UpdateAndRemoveEntity(request);
        if (success)
        {
            return Ok("Entity created successfully.");
        }
        return BadRequest("Failed to create entity.");
    }


    [HttpPut]
    [Route("dynamic-add-list-data")]
    [Authorize]
    public async Task<IActionResult> DynamicAddListData(List<DynamicListDataModel> request)
    {
        var success = await dynamicCreateObjectServices.DynamicAddListData(request);
        if (success)
        {
            return Ok("Entity created successfully.");
        }
        return BadRequest("Failed to create entity.");
    }


    [HttpPost]
    [Route("data-select")]
    public async Task<IActionResult> CreateWebDataSelect(DataSelect request)
    {
        context.WebDataSelect.Add(request);
        await context.SaveChangesAsync();
        return Ok("Oke");
    }

    [HttpPost]
    [Route("dynamic-add-parent-child-data")]
    //[Authorize]
    public async Task<IActionResult> DynamicAddParentChildData(DynamicRequestParentAdd request)
    {
        var success = await dynamicCreateObjectServices.DynamicAddParentAndChild(request);
        if (success)
        {
            return Ok("Entity created successfully.");
        }
        return BadRequest("Failed to create entity.");
    }

}
