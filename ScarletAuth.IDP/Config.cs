using Duende.IdentityServer.Models;

namespace ScarletAuth.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "roles",
                UserClaims = { "role" }
            },
            new IdentityResources.Email(),
            //{
            //    Name = "email",
            //    UserClaims = { "email", "email_verified" }
            //},
            new IdentityResources.Address()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
            new ApiScope("user.create"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource {
                Name = "weatherapi",
                Scopes = { "weatherapi.read", "weatherapi.write" },
                ApiSecrets = { new Secret("ScopeSecret".Sha256()) },
                UserClaims = { "role" }
            },
            new ApiResource {
                Name = "scarletauth.adminui.api",
                Scopes = { "scarletauth.adminui.api" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "mobile.client",
                ClientName = "Client Credentials Client",
                ClientSecrets = { new Secret("4A94979D-01D8-47EF-9299-5D7D74318A0E".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes = { "scope1" }
            },

            // web services api client
            new Client
            {
                ClientId = "services.client",
                ClientSecrets = { new Secret("A8579D08-95C1-4FE2-8BB9-A25445158AA8".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "mvc.interactive",
                ClientSecrets = { new Secret("D9859EEA-EBDF-4DCC-BC86-07865A6B0FFB".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            new Client
            {
                ClientId = "spa.interactive",
                ClientSecrets = { new Secret("D11F9E60-2A89-48F1-9D3B-F65E8CE1CB21".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            new Client
            {
                ClientId = "blazor.interactive",
                ClientSecrets = { new Secret("2D16BE15-949F-44DF-8DB6-0554FE038C43".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            new Client
            {
                ClientId = "scarletauth.adminui",
                ClientName = "scarletauth.adminui",
                ClientUri = "https://localhost:44303",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                ClientSecrets = { new Secret("A73560AE-8E8A-4508-80F6-B107EA362AB4".Sha256()) },
                RedirectUris = { "https://localhost:44303/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44303/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44303/signout-callback-oidc" },
                AllowedCorsOrigins = { "https://localhost:44303" },
                AllowedScopes = { "openid", "email", "profile", "roles" }
            },
            new Client
            {
                ClientId = "scarletauth.adminui_api_swaggerui",
                ClientName = "scarletauth.adminui_api_swaggerui",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris = { "https://localhost:44302/swagger/oauth2-redirect.html" },
                AllowedScopes = { "scarletauth.adminui_api" },
                AllowedCorsOrigins = { "https://localhost:44302" }
            }
        };
}
