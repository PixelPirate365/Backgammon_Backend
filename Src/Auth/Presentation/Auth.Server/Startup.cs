using Auth.Server.Entities;
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
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddControllersWithViews();
            services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OAuthIdentity")));
            services.AddIdentity<User, IdentityRole>(option => {
                option.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();
            var builder = services.AddIdentityServer(options => {
                options.EmitStaticAudienceClaim = true;
            }).AddConfigurationStore(opt => {
                opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("OAuth"),
                sql => sql.MigrationsAssembly(migrationAssembly));
            }).AddOperationalStore(opt => {
                opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("OAuth"),
                sql => sql.MigrationsAssembly(migrationAssembly));
            }).AddAspNetIdentity<User>();
            builder.AddDeveloperSigningCredential();
            services.Configure<DataProtectionTokenProviderOptions>(options => {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
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
