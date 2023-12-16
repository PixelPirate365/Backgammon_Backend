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
                            if (!context.IdentityResources.Any()) {
                                foreach (var dentityResource in AuthConfig.IdentityResourecs()) {
                                    context.IdentityResources.Add(dentityResource.ToEntity());
                                }
                                context.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex) {

                    }
                }
            }

            return host;
        }
    }
}
