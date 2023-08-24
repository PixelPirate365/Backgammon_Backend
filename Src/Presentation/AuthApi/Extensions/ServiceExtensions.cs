using Microsoft.OpenApi.Models;

namespace AuthApi.Extensions {
    public static class ServiceExtensions {
        public static void AddSwaggerExtension(this IServiceCollection services) {

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication API", Version = "v1" });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Type = SecuritySchemeType.ApiKey,
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
