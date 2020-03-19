using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MeasureUnitManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((cntx, config) =>
                {
                    var env = cntx.HostingEnvironment.EnvironmentName;
                    config.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true);
                })
                .UseStartup<Startup>()
                .UseKestrel();
        }

        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .MinimumLevel.Verbose()
                .CreateLogger();
        }
    }
}
