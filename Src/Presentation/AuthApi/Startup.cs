﻿using AuthApi.Extensions;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureApplication();
            services.AddIdentityAuthorization(Configuration);
            services.AddPersistence(Configuration);
            services.AddSwaggerExtension();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension(Configuration);

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
