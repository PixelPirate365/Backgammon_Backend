
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AuthApi {
    //public class Program {
    //    public static void Main(string[] args) {
    //        var builder = WebApplication.CreateBuilder(args);

    //        // Add services to the container.

    //        // builder.Services.AddControllers();
    //        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //        builder.Services.AddEndpointsApiExplorer();
    //        builder.Services.AddSwaggerGen();

    //        var app = builder.Build();

    //        // Configure the HTTP request pipeline.
    //        if (app.Environment.IsDevelopment()) {
    //            app.UseSwagger();
    //            app.UseSwaggerUI();
    //        }

    //        app.UseAuthorization();


    //        app.MapControllers();

    //        app.Run();
    //    }
    //}

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
                try {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    if (context.Database.IsSqlServer()) {
                        await context.Database.MigrateAsync();

                    }

                    logger.LogInformation("Host created.");
                }
                catch (Exception ex) {
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