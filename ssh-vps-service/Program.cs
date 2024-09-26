using ssh_vps_service;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
