using CliWrap;
using Microsoft.Extensions.Options;
using RabotUpdateManager.Abstractions;
using RabotUpdateManager.Options;

namespace RabotUpdateManager.Managers
{
    public class UpdateManager : IUpdateManager
    {
        private readonly ILogger<UpdateManager> logger;
        private readonly IOptions<EnviromentOptions> options;

        public UpdateManager(ILogger<UpdateManager> logger, IOptions<EnviromentOptions> options)
        {
            this.logger = logger;
            this.options = options;
        }

        public void CheckVersion()
        {

        }

        public async void CheckStatus()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(6));

            try
            {
                await Cli.Wrap("systemclt")
                    .WithArguments(options.Value.ServiceName + " status")
                    .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
                    .ExecuteAsync(cts.Token);
            }
            catch (Exception ex)
            {
                logger.LogInformation("Service non found {Exception}", ex);
            }
        }

        public void StopRabotService()
        {

        }

        public void StartRabotService()
        {

        }

        public void RemoveOldRabotDirectory()
        {

        }

        public void DownloadNewRabotVersion()
        {

        }
    }
}
