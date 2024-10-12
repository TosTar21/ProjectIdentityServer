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
                    AllowedScopes = { "api1", "roles" },
                    Claims = new List<ClientClaim> // Aquí agregamos el role al cliente
                    {
                        new ClientClaim("role", "DashboardAdmin")
                    }
                },
                new Client
                {
                    ClientId = "Errors",
                    ClientSecrets = { new Secret("errors_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api2", "roles" },
                    Claims = new List<ClientClaim> // Aquí agregamos el role al cliente
                    {
                        new ClientClaim("role", "ErrorsViewer")
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "Dashboard API"),
                new ApiScope("api2", "Errors API"),
                new ApiScope("roles", "Access roles")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("api1", "Dashboard API") { Scopes = { "api1", "roles" } },
                new ApiResource("api2", "Errors API") { Scopes = { "api2", "roles" } }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new [] { "role" })
            };
    }
}
