using Auth.Application.Modules;
using Auth.EmailService.Implementation;
using Auth.EmailService.Interfaces;
using Auth.EmailService.Models;
using Auth.MessageBus.Modules;
using Auth.Server.Entities;
using Auth.Server.Interfaces;
using Auth.Server.Interfaces.Repository;
using Auth.Server.Services;
using Auth.Server.Services.Repository;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Auth.Server {
    public class Startup {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services) {
            services.AddAutoMapper(typeof(Startup));
            var emailConfig = Configuration.GetSection(nameof(EmailConfiguration)).Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddSingleton<IEmailSender, EmailSender>();
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddControllersWithViews();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OAuthIdentity")));
            services.AddIdentity<User, IdentityRole>(option => {
                option.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<IProfileService, ProfileService>();
            var builder = services.AddIdentityServer(options => {
                options.EmitStaticAudienceClaim = true;
            }).AddConfigurationStore(opt => {
                opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("OAuth"),
                sql => sql.MigrationsAssembly(migrationAssembly));
            }).AddOperationalStore(opt => {
                opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("OAuth"),
                sql => sql.MigrationsAssembly(migrationAssembly));
            }).AddAspNetIdentity<User>()
            .AddProfileService<ProfileService>();
            builder.AddDeveloperSigningCredential();
            services.Configure<DataProtectionTokenProviderOptions>(options => {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });
            services.AddMessageBus(Configuration);
            services.ConfigureApplication();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) => {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; connect-src 'self' wss://localhost:44364;");
                await next();
            });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
            });
        }

    }
}
