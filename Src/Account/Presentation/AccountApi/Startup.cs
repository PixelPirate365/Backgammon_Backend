using AccountApi.Consumers;
using AccountApi.Extensions;
using AccountApi.Filters;
using AccountService.Application.Modules;
using AccountService.Common.Options.RabbitMQ;
using AccountService.Common.Settings;
using AccountService.Persistence.Modules;
namespace AccountApi {
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
            AccountApiSettings.ApiBaseUrl = Configuration[$"{nameof(AccountApiSettings)}:{nameof(AccountApiSettings.ApiBaseUrl)}"];
            AccountApiSettings.ImageRootPath = _env.WebRootPath;
            services.AddSwaggerExtension();
            services.ConfigureApplication();
            var rabbitMQOptions = new RabbitMQOptions();
            Configuration.GetSection(nameof(RabbitMQOptions)).Bind(rabbitMQOptions);
            services.AddSingleton(rabbitMQOptions);
            services.AddPersistence(Configuration);
            services.AddHostedService<AccountCreationEventConsumer>();
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
            //app.UseRabbitMQBusConsumer();
        }
    }
}
