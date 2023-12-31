
using AccountService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AccountApi {
    public class Program {
        public static async Task Main(string[] args) {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            var host = CreateHostBuilder(args).UseDefaultServiceProvider(x => x.ValidateScopes = false).Build();
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                try {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    if (context.Database.IsSqlServer()) {
                        await context.Database.MigrateAsync();
                    }
                    await ApplicationDbContextSeed.SeedAccountData(context);
                    logger.LogInformation("Host created.");
                }
                catch (Exception ex) {
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                    throw;
                }
            }
            await host.RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
             .UseSerilog() //Uses Serilog instead of default .NET Logger
                 .ConfigureWebHostDefaults(webBuilder => {
                     webBuilder.UseStartup<Startup>();
                 });

    }
}