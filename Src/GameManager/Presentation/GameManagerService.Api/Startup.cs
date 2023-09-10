﻿using GameManagerApi.Extensions;
using GameManagerApi.Filters;
using GameManagerService.Common.Settings;
using GameManagerService.Application.Modules;
using GameManagerService.Persistence.Modules;
using GameManagerService.Identity.Modules;
namespace GameManagerApi {
    public class Startup {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            Configuration = configuration;
            _env = env;
        }
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            AuthenticationApiSettings.ApiBaseUrl = Configuration[$"{nameof(AuthenticationApiSettings)}:{nameof(AuthenticationApiSettings.ApiBaseUrl)}"];
            services.AddSwaggerExtension();
            services.ConfigureApplication();

            services.AddPersistence(Configuration);
            services.AddIdentityAuthorization(Configuration);

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<JwtForwardingMiddleware>();
            app.UseSwaggerExtension(Configuration);
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}