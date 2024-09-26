using Renci.SshNet;
using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace ssh_vps_service
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _config;

        private readonly SshClient _ssh;

        public Worker(IConfiguration config)
        {
            this._config = config;
            _ssh = new SshClient(_config["ConnectionData:Hostname"], _config["ConnectionData:User"], new PrivateKeyFile(_config["ConnectionData:IdentityFile"]));
            _ssh.KeepAliveInterval = new TimeSpan(0, 0, 10);
            
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _ssh.Connect();
            _ssh.AddForwardedPort(new ForwardedPortDynamic(UInt32.Parse(_config["ConnectionData:ForwardingPort"])));
            _ssh.ForwardedPorts.First().Start();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ssh.ForwardedPorts.First().Stop();
            _ssh.Disconnect();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
