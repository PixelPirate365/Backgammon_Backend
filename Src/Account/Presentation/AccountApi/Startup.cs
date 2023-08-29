using AccountApi.Extensions;
using AccountApi.Filters;
using AccountService.Application.Modules;
using AccountService.Persistence.Modules;
namespace AccountApi {
    public class Startup {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddSwaggerExtension();
            services.ConfigureApplication();
            services.AddPersistence(Configuration);
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
