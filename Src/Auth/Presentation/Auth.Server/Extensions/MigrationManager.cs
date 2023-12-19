using Auth.Server.Config;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Extensions {
    public static class MigrationManager {
        public static IHost MigrateDatabase(this IHost host) {
            using (var scope = host.Services.CreateScope()) {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>()) {
                    try {

                        context.Database.Migrate();
                        if (!context.Clients.Any()) {
                            foreach (var client in AuthConfig.Clients()) {
                                context.Clients.Add(client.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        if (!context.IdentityResources.Any()) {
                            foreach (var dentityResource in AuthConfig.IdentityResources()) {
                                context.IdentityResources.Add(dentityResource.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        var apiScopes = context.ApiScopes.ToList();
                        var newApiScopes = AuthConfig.ApiScopes()
                            .Where(x => !apiScopes.Select(x => x.Name).Contains(x.Name)).ToList();
                        foreach(var newApiScope in newApiScopes) {
                            context.ApiScopes.Add(newApiScope.ToEntity());
                        }
                        context.SaveChanges();
                        var apiResources = context.ApiResources.ToList();
                        var newApiResources = AuthConfig.ApiResources()
                            .Where(x => !apiResources.Select(x => x.Name).Contains(x.Name)).ToList();
                        foreach (var newApiResource in newApiResources) {
                            context.ApiResources.Add(newApiResource.ToEntity());
                        }
                        context.SaveChanges();
                    }
                    catch (Exception ex) {

                    }
                }

            }

            return host;
        }
    }
}
