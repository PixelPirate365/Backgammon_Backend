using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGateway {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
        }
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapGet("/",async context => {
                await context.Response.WriteAsync("Hello World!");
            }));
            await app.UseOcelot();

        }
    }
}
