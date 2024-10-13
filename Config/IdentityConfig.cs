using IdentityServer4.Models;

namespace Config
{
    public static class IdentityConfig
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "Dashboard",
                    ClientSecrets = { new Secret("dashboard_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "dashboard_api" }
                },
                new Client
                {
                    ClientId = "LogErrores",
                    ClientSecrets = { new Secret("logerrores_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "logerrores_api" } 
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("dashboard_api", "Dashboard API"),
                new ApiScope("logerrores_api", "Log Errores API") 
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("dashboard_api", "Dashboard API") { Scopes = { "dashboard_api" } },
                new ApiResource("logerrores_api", "Log Errores API") { Scopes = { "logerrores_api" } }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
    }
}
