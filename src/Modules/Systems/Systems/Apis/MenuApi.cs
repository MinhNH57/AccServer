using Systems.Infrastructure.Entities;

namespace Systems.Apis;

public class MenuApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("System");

        var api = vApi.MapGroup("menu").HasApiVersion(1.0);

        api.MapGet("/get-all", GetMenu)
            .WithName("get-all")
            .WithSummary("Lấy ra tất cả menu")
            .WithDescription("Lấy ra tất cả menu")
            .WithTags("Systems")
            .RequireAuthorization();

        api.MapGet("/get-by-code-user/{codeUser}", GetMenuByUser)
            .WithName("get-by-code-user")
            .WithSummary("Lấy menu theo quyền user")
            .WithDescription("Lấy menu theo quyền user")
            .WithTags("Systems")
            .RequireAuthorization();
    }

    private async Task<IResult> GetMenuByUser(
        [AsParameters] SystemService service,
        [FromHeader(Name = TenantConstant.TenantIdHeader)][Required]
        string tenantId,
        [Description("Mã người dùng")] string codeUser,
        CancellationToken token)
    {
        var tenant = service.TenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
        var data = await service.Cache.GetOrCreateAsync($"KT:{tenant.Identifier}:menu:{codeUser}", async () =>
        {
            var lstMenus = await GetFromDb(service, codeUser, token);
            var menuRes = ConvertMenu(lstMenus);
            return menuRes;
        }, token: token);

        return Results.Ok(data);
    }

    private async Task<List<Menu>> GetFromDb(
        [AsParameters] SystemService service,
        string codeUser, CancellationToken token)
    {
        string? roleUser = service.CurrentUser.Role;

        if (roleUser == "ADMIN")
        {
            var lstData = await service.DbContext.WSmartMenu
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
                }).ToListAsync( token);
            return lstData;
        }
        var menuQuery = service.DbContext.WSmartMenu
            .AsNoTracking()
            .Where(c => c.IsActive == true);

        var rulesUserQuery = service.DbContext.RuleUser
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
                                   IconWeb = m.IconWeb,
                                   LeftMenu = m.LeftMenu,
                               }).ToListAsync(token);

        return joinQuery;
    }

    private async Task<IResult> GetMenu(
        [AsParameters] SystemService service,
    [FromHeader(Name = TenantConstant.TenantIdHeader)][Required]
    string tenantId)
    {
        var tenant = service.TenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
        string query = "select * from WSmartMenu where IsActive = 1";
        var lstData = await service.SmartDataServices.GetListObject<Menu>(query, tenant.ConnectionString());
        //var lstData = services.ReadManyNoTracked<Menu>().AsNoTracking().Where(x => x.IsActive == true).OrderBy(c => c.MenuLevel)
        //    .ToList();
        var menuRes = ConvertMenu(lstData);
        return Results.Ok(menuRes);
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
            Url = menu.MenuParameters,
            Icons = menu.IconWeb,
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
                        Options = [.. grandMenu.Skip(5).Take(5)],
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
    public string? Icons { get; set; }
    public string? Value { get; set; }
    public string? Url { get; set; }
    public int Count { get; set; }
    public string? About { get; set; }
    public string? Id { get; set; }
}