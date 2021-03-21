using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Prometheus.DotNetRuntime;

namespace StatsWeb.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IDisposable collector = DotNetRuntimeStatsBuilder
                .Customize()
                .WithContentionStats()
                .WithJitStats()
                .WithThreadPoolSchedulingStats()
                .WithThreadPoolStats()
                .WithGcStats()
                .WithExceptionStats()
                .StartCollecting();
            
            CreateHostBuilder(args).Build().Run();
            collector.Dispose();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}