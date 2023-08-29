
using AuthService.Domain.Entities;
using AuthService.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AuthApi {


    public class Program {
        public static async Task Main(string[] args) {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                try {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    if (context.Database.IsSqlServer()) {
                        await context.Database.MigrateAsync();
                    }
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await ApplicationDbContextSeed.ApplicationDataSeed(context, userManager);
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