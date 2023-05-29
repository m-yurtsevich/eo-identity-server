using IdentityServer4.Models;
using IdentityServer4;

namespace EO.IdentityServer.IdentityConfiguration
{
    public static class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("eventorganizerapi", "Event Organizer API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("eventorganizerapi")
                {
                    Scopes = { "eventorganizerapi" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // React client
                new Client
                {
                    ClientId = "eventorganizer",
                    ClientName = "Event Organizer",
                    ClientUri = "https://localhost:3000",

                    AllowedGrantTypes = GrantTypes.Implicit,

                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "https://localhost:3000/signin-oidc",
                    },

                    PostLogoutRedirectUris = { "https://localhost:3000/signout-oidc" },
                    AllowedCorsOrigins = { "https://localhost:3000" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "eventorganizerapi"
                    },

                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
