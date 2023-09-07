using GameManagerService.Common.Options.Swagger;

namespace GameManagerApi.Extensions {
    public static class ApiExtensions {
        public static void UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration) {
            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option => {
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(option => {
                option.SwaggerEndpoint(swaggerOptions.UiEndPoint, swaggerOptions.Description);
            });
        }
    }
}
