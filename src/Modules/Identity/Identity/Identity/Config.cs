using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity.Identity;

public class Config
{
    //public static IEnumerable<Client> Clients
    //    =>
    //    [
    //        new()
    //        {
    //            ClientId = "Catalog.Client",
    //            AllowedGrantTypes = GrantTypes.ClientCredentials,
    //            ClientSecrets =
    //            [
    //                new Secret("secret".Sha256())
    //            ],
    //            AllowedScopes = { "Catalog.Api" }
    //        },
    //        new()
    //        {
    //            ClientId = "movies_mvc_client",
    //            ClientName = "Movies MVC Web App",
    //            AllowedGrantTypes = GrantTypes.Code,
    //            AllowRememberConsent = false,
    //            RedirectUris = new List<string>()
    //            {
    //                "https://localhost:5002/signin-oidc"
    //            },
    //            PostLogoutRedirectUris = new List<string>()
    //            {
    //                "https://localhost:5002/signout-callback-oidc"
    //            },
    //            ClientSecrets = new List<Secret>()
    //            {
    //                new Secret("secret".Sha256())
    //            },
    //            AllowedScopes = new List<string>()
    //            {
    //                "openid",
    //                "profile"
    //            }
    //        }
    //    ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("catalog", "Catalog API"),
            new ApiScope("cms", "CMS API"),
            new ApiScope("acc", "ACC API"),
            new ApiScope("catalog.read"),
            new ApiScope("catalog.write"),
        ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("catalog", "Catalog API")
        {
            Scopes = { "catalog.read", "catalog.write" },
            // additional claims to put into access token
            UserClaims = { "code_user", "code_unit" }
        }
    ];

    public static IEnumerable<IdentityResource> IdentityResources
        => [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<Client> Clients =>
    [
        new()
        {
            ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            //RefreshTokenExpiration = TokenExpiration.Absolute,
            //AbsoluteRefreshTokenLifetime = 500000,
            //RefreshTokenUsage = TokenUsage.ReUse,
            AccessTokenLifetime = 60,
            AllowOfflineAccess = true,
            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            // scopes that client has access to
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                Constants.StandardScopes.Catalog,
                Constants.StandardScopes.Cms,
                "acc"
            }
        },
        new()
        {
            ClientId = "smartacc",
            AllowedGrantTypes = GrantTypes.Code,
            //AllowRememberConsent = false,
            RedirectUris = new List<string>()
            {
                "https://acctest.ssoftware.vn/login"
            },
            RequireClientSecret = false,
            RequirePkce = false,
            //PostLogoutRedirectUris = new List<string>()
            //{
            //    "https://localhost:5002/signout-callback-oidc"
            //},
            //ClientSecrets = new List<Secret>()
            //{
            //    new Secret("secret".Sha256())
            //},
            AllowedScopes = new List<string>()
            {
                IdentityServerConstants.StandardScopes.OpenId,
                Constants.StandardScopes.Catalog
            }
        }
    ];
}