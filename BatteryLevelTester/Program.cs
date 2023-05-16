using AppDrones.BatteryLevelTester;
using AppDrones.Core.Dto;
using AppDrones.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.Configure<BatteryLevelTesterSettings>(ctx.Configuration.GetSection("BatteryLevelTesterSettings"));
        services.AddDbContext<DatabaseContext>();
        services.AddHostedService<WorkerService>();
    }
).Build();

await host.RunAsync();