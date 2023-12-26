using AccountApi.Consumers;
using AccountApi.Extensions;
using AccountApi.Filters;
using AccountService.Application.Modules;
using AccountService.Common.Options.RabbitMQ;
using AccountService.Common.Settings;
using AccountService.Persistence.Modules;
using AccountService.Identity.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AccountService.MessageBus.Modules;

namespace AccountApi
{
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option
                => {
                    option.RequireHttpsMetadata = false;
                    option.Audience = authServerSettings.Audience;
                    option.Authority = authServerSettings.Authority;
                });
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            AccountApiSettings.ApiBaseUrl = Configuration[$"{nameof(AccountApiSettings)}:{nameof(AccountApiSettings.ApiBaseUrl)}"];
            AccountApiSettings.ImageRootPath = _env.WebRootPath;
            services.AddSwaggerExtension();
            services.ConfigureApplication();
            var rabbitMQOptions = new RabbitMQOptions();
            Configuration.GetSection(nameof(RabbitMQOptions)).Bind(rabbitMQOptions);
            services.AddSingleton(rabbitMQOptions);
            services.AddPersistence(Configuration);
            services.AddIdentityAuthorization(Configuration);
            services.AddMessageBus(Configuration);
            services.AddHostedService<UserProfileCreationEventConsumer>();
            services.AddHostedService<AccountSoftDeleteEventConsumer>();

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
           // app.UseMiddleware<JwtForwardingMiddleware>();
            app.UseSwaggerExtension(Configuration);
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
