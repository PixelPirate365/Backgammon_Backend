using AuthService.Application.Interfaces;
using AuthService.Identity.Models;
using AuthService.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Identity.Modules {
    public static class ApplicationModule {
        public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services, IConfiguration configuration) {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            InitializeJwtTokenParameters(services, configuration);
            return services;
        }
        private static void InitializeJwtTokenParameters(IServiceCollection services, IConfiguration configuration) {
            var appSettingsSection = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(appSettingsSection);
            // configure jwt authentication
            var jwtSettings = appSettingsSection.Get<JwtSettings>();
            var tokenValidationParameters = new TokenValidationParameters {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

            });
        }
    }
}
