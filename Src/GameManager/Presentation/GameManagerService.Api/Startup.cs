using GameManagerApi.Extensions;
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
            var authServerSettings = Configuration.GetSection(nameof(AuthServerSettings))
                .Get<AuthServerSettings>();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt => {
                    opt.RequireHttpsMetadata = false;
                    opt.Authority = authServerSettings.Authority;
                    opt.Audience = authServerSettings.Audience;
                });
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            app.UseSwaggerExtension(Configuration);
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
