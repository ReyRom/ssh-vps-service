using Renci.SshNet;
using System.Diagnostics;

namespace ssh_vps_service
{
    public class Worker : BackgroundService
    {
        private readonly Process process = new System.Diagnostics.Process();

        //private readonly SshClient ssh = new SshClient();

        public Worker()
        {
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c ssh -NC -D 8888 vps";
            process.StartInfo.CreateNoWindow = true;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            process.Start();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            process.Kill();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine(await process.StandardOutput.ReadLineAsync());
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
