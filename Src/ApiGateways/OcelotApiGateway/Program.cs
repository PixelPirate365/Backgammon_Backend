using OcelotApiGateway;

public class Program {
    public static void Main(string[] args) {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext,config) => {
                config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json",true,true);
            })
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();

            }).ConfigureLogging((hostingContext,loggingBuilder) => {
                loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
    }
}
