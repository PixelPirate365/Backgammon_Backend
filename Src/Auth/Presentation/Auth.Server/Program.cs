using Auth.Server;
using Auth.Server.Extensions;
using Auth.Server.Interfaces;
using Auth.Server.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

public class Program {
    public static void Main(string[] args) {
        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                    .CreateLogger();
        var builder = CreateHostBuilder(args).Build();
        var config = builder.Services.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("OAuthIdentity");
        SeedUserData.InsertSeedData(connectionString);
        builder.MigrateDatabase().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext ,services ) => {
            //services.AddScoped<ICurrentUserService, CurrentUserService>();
        })
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
}