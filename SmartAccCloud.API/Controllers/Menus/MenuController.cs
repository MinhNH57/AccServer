using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Domain.Entity.Menus;
using SmartAccCloud.Domain.Entity.Rules;
using SmartAccCloud.Infrastructure.Caching;

namespace SmartAccCloud.API.Controllers.Menus;

[Route("api/menu")]
[Route("api/v{v:apiVersion}/menu")]
[ApiVersion(1)]
[ApiController]
[Authorize]
public class MenuController(
    ICrudServicesAsync services,
    IApplicationDbContext dbContext,
    IDataServices dataServices,
    ICurrentUser currentUser,
    IDistributedCache cache,
    IMultiTenantContextAccessor tenantContextAccessor) : ResultControllerBase
{
    TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    [MapToApiVersion(1)]
    [HttpGet]
    [Route("get-all")]
    public async Task<IResult> GetMenu()
    {
        string query = "select * from WSmartMenu where IsActive = 1";
        var lstData = await dataServices.GetListObject<Menu>(query, _tenant.ConnectionString());
        //var lstData = services.ReadManyNoTracked<Menu>().AsNoTracking().Where(x => x.IsActive == true).OrderBy(c => c.MenuLevel)
        //    .ToList();
        var menuRes = ConvertMenu(lstData);

        return Results.Ok(menuRes);
    }

    [HttpGet]
    [Route("get-by-code-user/{codeUser}")]
    public async Task<IActionResult> GetMenuByUser(string codeUser, CancellationToken token)
    {
        var data = await cache.GetOrCreateAsync($"KT:menu:{_tenant.Identifier}:{codeUser}", async () =>
        {
            var lstMenus = await GetFromDb(codeUser, token);
            var menuRes = ConvertMenu(lstMenus);
            return menuRes;
        }, token: token);

        return Ok(data);
    }


    private async Task<List<Menu>> GetFromDb(string codeUser, CancellationToken token)
    {
        string? roleUser = currentUser.Role;

        if (roleUser == "ADMIN")
        {
            var lstData = services.ReadManyNoTracked<Menu>()
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(c => c.MenuLevel)
                .Select(v => new Menu()
                {
                    MenuCaption = v.MenuCaption,
                    MenuName = v.MenuName,
                    MenuLevel = v.MenuLevel,
                    ParentMenu = v.ParentMenu,
                    MenuParameters = v.MenuParameters,
                    IconWeb = v.IconWeb,
                    LeftMenu = v.LeftMenu
                }).ToList();
            return lstData;
        }
        var menuQuery = services.ReadManyNoTracked<Menu>()
            .AsNoTracking()
            .Where(c => c.IsActive == true);

        var rulesUserQuery = services.ReadManyNoTracked<RuleUser>()
            .AsNoTracking()
            .Where(c => c.CodeUser == codeUser && c.AllowView);

        var joinQuery = await (from m in menuQuery
                               join r in rulesUserQuery on m.MenuName equals r.KeyFuntion
                               orderby m.MenuLevel
                               select new Menu()
                               {
                                   MenuCaption = m.MenuCaption,
                                   MenuName = m.MenuName,
                                   MenuLevel = m.MenuLevel,
                                   ParentMenu = m.ParentMenu,
                                   MenuParameters = m.MenuParameters,
                                   LeftMenu = m.LeftMenu
                               }).ToListAsync(token);

        return joinQuery;
    }

    private MenuResponse ConvertMenu(List<Menu> menus)
    {
        var leftMenus = new List<MenuItem>();
        var megaMenus = new List<MegaMenu>();

        var listRootMenu = menus.Where(c => string.IsNullOrEmpty(c.ParentMenu)).ToList();

        // Hàm tạo MenuItem thay cho  c=> new ()
        MenuItem CreateMenuItem(Menu menu) => new MenuItem
        {
            Id = menu.MenuName,
            Text = menu.MenuCaption,
            Url = menu.MenuParameters,
            Icon = menu.IconWeb
        };

        // Hàm tạo MegaMenu
        MegaMenu CreateMegaMenu(Menu menu) => new MegaMenu
        {
            Value = menu.MenuCaption,
            Id = menu.MenuName,
            Url = menu.MenuParameters
        };

        foreach (var rootMenu in listRootMenu)
        {
            if (rootMenu is { LeftMenu: true })
            {
                var rootMenuBlazorZone = new MenuItem()
                {
                    Id = rootMenu.MenuName,
                    Text = rootMenu.MenuCaption,
                    Url = rootMenu.MenuParameters,
                    Icon = rootMenu.IconWeb,
                };
                leftMenus.Add(rootMenuBlazorZone);
                var childMenus = menus
                    .Where(c => c.ParentMenu == rootMenu.MenuName)
                        .Select(CreateMenuItem);

                rootMenuBlazorZone.Items = [.. childMenus];
            }
            else
            {
                var categoryModel = CreateMegaMenu(rootMenu);

                categoryModel.Options = menus
                    .Where(c => c.ParentMenu == rootMenu.MenuName)
                    .Select(CreateMegaMenu)
                    .ToList();
                megaMenus.Add(categoryModel);
            }
        }

        foreach (var item in megaMenus)
        {
            int count = 1;
            var menuWarpSplits = new List<MegaMenu>();
            foreach (var itemOption in item.Options!)
            {
                itemOption.Count = count++;
                var grandMenu = menus.Where(c => c.ParentMenu == itemOption.Id).Select(CreateMegaMenu).ToList();
                if (grandMenu.Count > 5)
                {
                    var menuWarp = grandMenu.Take(5);

                    itemOption.Options = [.. menuWarp];
                    menuWarpSplits.Add(new MegaMenu
                    {
                        Count = count++,
                        Value = "",
                        Options = [.. grandMenu.Skip(5).Take(5)]
                    });
                }
                else
                {
                    itemOption.Options = [.. grandMenu];
                }

            }
            item.Options.AddRange(menuWarpSplits);
            item.Options = item.Options.OrderBy(c => c.Count).ToList();
        }

        foreach (var menu in leftMenus)
        {
            foreach (var chidItem in menu.Items)
            {
                var grandMenu = menus.Where(c => c.ParentMenu == chidItem.Id).Select(CreateMenuItem);
                chidItem.Items = new List<MenuItem>(grandMenu);
            }
        }

        return new MenuResponse(leftMenus, megaMenus);
    }
}



record MenuResponse(List<MenuItem> LeftMenus, List<MegaMenu> MegaMenus);

class MenuItem
{
    public string? Text { get; set; }
    public string? Id { get; set; }
    public string? Url { get; set; }
    public string? ParentId { get; set; }
    public string? Icon { get; set; }
    public List<MenuItem> Items { get; set; } = new();
}
public class MegaMenu
{
    public List<MegaMenu>? Options { get; set; }
    public string? Value { get; set; }
    public string? Url { get; set; }
    public int Count { get; set; }
    public string? About { get; set; }
    public string? Id { get; set; }
}