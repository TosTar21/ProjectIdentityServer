using Duende.IdentityServer.Models;


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
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Habilita el flujo ROPC
            AllowedScopes = { "dashboard_api", "roles", "offline_access" },
            AllowOfflineAccess = true  // Permite refresh tokens
        },
        new Client
        {
            ClientId = "LogErrores",
            ClientSecrets = { new Secret("logerrores_secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,  // Habilita el flujo ROPC
            AllowedScopes = { "logerrores_api", "role_access", "offline_access" },
            AllowOfflineAccess = true
        }
    };


        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("dashboard_api", "Dashboard API"),
                new ApiScope("logerrores_api", "Log Errores API"),
                new ApiScope("role_access", "Access to user roles") // Renombrar el API scope
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("dashboard_api", "Dashboard API") { Scopes = { "dashboard_api", "role_access" } }, // Usa el nuevo nombre
                new ApiResource("logerrores_api", "Log Errores API") { Scopes = { "logerrores_api", "role_access" } } // Usa el nuevo nombre
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] { "role" }) 
            };
    }
}
