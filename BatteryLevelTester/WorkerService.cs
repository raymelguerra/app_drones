using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Options;
using AppDrones.Core.Dto;
using AppDrones.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppDrones.BatteryLevelTester
{
    public class WorkerService : BackgroundService
    {
        private readonly BatteryLevelTesterSettings _settings;
        private readonly DatabaseContext _context;
        public WorkerService(IOptions<BatteryLevelTesterSettings> options, DatabaseContext context)
        {
            _settings = options.Value;
            _context = context;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckBatteryStatus();
                TimeSpan frequency = TimeSpan.FromMinutes(_settings.RunFrequency);
                await Task.Delay(frequency, stoppingToken);
            }
        }

        private async Task CheckBatteryStatus()
        {
            var drones = await _context.Drone.ToListAsync();
            foreach (var item in drones)
            {
                string info = $"Battery level: Drone serial number <{item.SerialNumber}> --- Battery percent: {item.BatteryCapacity}%";
                WriteInLog(info);
            }
        }

        private void WriteInLog(string message)
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string LogPath = System.IO.Path.Join(folder, "logs\\history.txt");

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File(LogPath, rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            Log.Information(message);

            Log.CloseAndFlush();

        }
    }
}
