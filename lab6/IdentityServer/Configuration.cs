using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client_id_lab",
                ClientSecrets = new List<Secret>{new Secret("client_secret_lab".ToSha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                {
                    "DatabaseAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                RedirectUris = {"http://localhost:5000/signin-oidc"},
                AlwaysIncludeUserClaimsInIdToken = true
            }
        };

        public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new ApiScope("DatabaseAPI")
        };

        public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource
            {
                Name = "LabResource",
                Scopes = new List<string>
                {
                    "DatabaseAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
