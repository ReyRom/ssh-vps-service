using Microsoft.Extensions.Hosting.WindowsServices;

using ssh_vps_service;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json"));

builder.ConfigureServices(services => services.AddHostedService<Worker>());

builder.UseWindowsService();

var host = builder.Build();
host.Run();
