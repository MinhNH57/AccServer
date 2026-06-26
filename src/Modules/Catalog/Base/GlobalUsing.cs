global using Dapper;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Logging;
global using Newtonsoft.Json;

global using BuildingBlocks.Caching;
global using BuildingBlocks.MultiTenancy;
global using BuildingBlocks.Pagination.Version1;
global using Carter;
global using Catalog.Base.Features.Queries.GetCatalogDynamic;
global using Catalog.Base.Infrastructure;
global using Catalog.Base.Infrastructure.StoredProcedures;