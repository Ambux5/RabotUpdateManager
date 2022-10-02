using CliWrap;
using CliWrap.Buffered;
using Microsoft.Extensions.Options;
using RabotUpdateManager.Abstractions;
using RabotUpdateManager.Options;
using System.Text;

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

        /// <summary>
        /// Update Rabot Rover APi
        /// </summary>
        public void UpdateNewRabotVersion()
        {
            _ = DownloadNewRabotVersionAsync("admin", "password",options.Value.ArtifactUrl,options.Value.Version, options.Value.FileName);
            StopRabotService("robot.service");
            RemoveRabotService("robot.service");
            GenerateNewRabotService("robot.service",options.Value.serviceWorkingDirectory);
            StartRabotService("robot.service");
        }

        public void CheckNewVersion()
        {
            /*
             * 1. compare current and version in artifactory
             * 2. if version in artifactory is newer call UpdateNewRabotVersion or do nothing
             * 
             */
        }


        /// <summary>
        /// Donwload RabotRover API and unzip
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="url">artifactory url address</param>
        /// <param name="version"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private async Task DownloadNewRabotVersionAsync(string user,string password,string url, string version, string FileName)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(6));
            string downloadsting = $"--user={user} --password={password} -d {url}";
            Console.WriteLine(downloadsting);

            try
            {
                await Cli.Wrap("wget")
                    .WithArguments(downloadsting)
                    .ExecuteBufferedAsync();
            }
            catch (Exception ex)
            {
                //logger.LogInformation("Download failed");
                Console.WriteLine("Download failed");
                return;
            }
            //logger.LogInformation("Download file is done");
            Console.WriteLine("Download file is done");


            string unzipCommand = $"-o {FileName} -d /home/pi/Rabot_v_{version}";
            Console.WriteLine(unzipCommand);

            try
            {
                await Cli.Wrap("unzip")
                    .WithArguments(unzipCommand)
                    .ExecuteBufferedAsync();
            }
            catch (Exception ex)
            {
                //logger.LogInformation("Download failed");
                Console.WriteLine("Unzip  failed");
                return;
            }
            //logger.LogInformation("Unzip file is done");
            Console.WriteLine("Unzip file is done");
        }

        private void StopRabotService(string serviceName)
        {
            //try
            //{
            //    await Cli.Wrap("systemctl")
            //        .WithArguments($"stop {serviceName}")
            //        .ExecuteBufferedAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("something went wrong" + ex);
            //}

            //try
            //{
            //    await Cli.Wrap("systemctl")
            //        .WithArguments($"disable {serviceName}")
            //        .ExecuteBufferedAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("something went wrong" + ex);
            //}
        }

        private void RemoveRabotService(string serviceName)
        {
            //try
            //{
            //    await Cli.Wrap("rm")
            //        .WithArguments($"/lib/systemd/system/{serviceName}")
            //        .ExecuteBufferedAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("something went wrong" + ex);
            //}
        }

        private void GenerateNewRabotService(string serviceName, string serviceWorkingDirectory)
        {
            string description = "My Service";
            string target = "network-online.target";
            string serviceType = "simple";
            string serviceRestart = "always";
            string serviceExecStart = $"{serviceWorkingDirectory}RabotRoverApp";
            string installWantedBy = "multi-user.target";


            StringBuilder builder = new StringBuilder("[Unit]");
            builder.AppendLine();

            builder.AppendFormat($"Descripion={description}", description);   //writes headerName as usual. Not sure what items[0] does
            builder.AppendLine();

            builder.AppendFormat($"Wants={target}", target);   //writes headerName as usual. Not sure what items[0] does
            builder.AppendLine();

            builder.AppendFormat($"After={target}", target);   //writes headerName as usual. Not sure what items[0] does
            builder.AppendLine();
            builder.AppendLine();

            builder.AppendFormat("[Service]");
            builder.AppendLine();
            builder.AppendFormat($"Type={serviceType}", serviceType);
            builder.AppendLine();
            builder.AppendFormat($"Restart={serviceRestart}", serviceRestart);
            builder.AppendLine();
            builder.AppendFormat($"WorkingDirectory={serviceWorkingDirectory}", serviceWorkingDirectory);
            builder.AppendLine();
            builder.AppendFormat($"ExecStart={serviceExecStart}", serviceExecStart);
            builder.AppendLine();
            builder.AppendLine();

            builder.AppendFormat("[Install]");
            builder.AppendLine();
            builder.AppendFormat($"WantedBy={installWantedBy}", installWantedBy);


            string result = builder.ToString();
            File.WriteAllText(serviceName, result);

            //try
            //{
            //    await Cli.Wrap("mv")
            //        .WithArguments($"{serviceName} /lib/systemd/system/{serviceName}")
            //        .ExecuteBufferedAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("something went wrong" + ex);
            //}
        }

        private void StartRabotService(string serviceName)
        {
            //try
            //{
            //    await Cli.Wrap("systemctl")
            //        .WithArguments($"enable {serviceName}")
            //        .ExecuteBufferedAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("something went wrong" + ex);
            //}

            //try
            //{
            //    await Cli.Wrap("systemctl")
            //        .WithArguments($"start {serviceName}")
            //        .ExecuteBufferedAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("something went wrong" + ex);
            //}
        }



    }
}
