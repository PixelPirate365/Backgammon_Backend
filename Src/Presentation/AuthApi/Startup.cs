using AuthApi.Extensions;
using AuthApi.Filters;
using AuthService.Application.Modules;
using AuthService.Common.Constants;
using AuthService.Identity.Modules;
using AuthService.Persistence.Modules;
using Microsoft.OpenApi.Models;
namespace AuthApi {
    public class Startup {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddSwaggerGen(c => {
                c.AddServer(new OpenApiServer { Url = "" });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = SwaggerConstants.AuthenticationApi, Version = "v1" });
                c.MapType<Guid>(() => new OpenApiSchema { Type = "string", Format = null });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureApplication();
            services.AddIdentityAuthorization(Configuration);
            services.AddPersistence(Configuration);

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseSwaggerExtension(Configuration);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
