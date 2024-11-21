namespace AlspecBackend.Api
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static readonly string ServiceName = typeof(Program).Namespace!;

        public static int Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
            var configuration = BuildConfiguration(environment);

            try
            {
                BuildWebHost(args, configuration).Run();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex} {ServiceName} terminated unexpectedly");
                return 1;
            }
        }

        private static IConfigurationRoot BuildConfiguration(string environment) =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.ToLowerInvariant()}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

        private static IWebHost BuildWebHost(string[] args, IConfiguration configuration) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .Build();
    }
}