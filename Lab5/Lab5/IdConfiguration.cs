using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Lab5
{
    public class IdConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("Api", "Web Api")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("LabWebsite", "Lab Website", new []
            {
                JwtClaimTypes.Name
            })
            {
                Scopes = {"LabWebsite"}
            }
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "lab-website",
                ClientName = "LabWebsite",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "/Home"
                },
                AllowedCorsOrigins =
                {
                    "/Home"
                },
                PostLogoutRedirectUris =
                {
                    "/Home"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "LabWebsite"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}
