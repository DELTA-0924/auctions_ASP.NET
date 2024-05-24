using Auctions_Web_API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Auctions_Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args).UseSerilog((hostingContext, loggerConfiguration) =>
              {
                  loggerConfiguration
                      .ReadFrom.Configuration(hostingContext.Configuration);
              }).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
