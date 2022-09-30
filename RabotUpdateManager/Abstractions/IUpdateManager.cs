namespace RabotUpdateManager.Abstractions
{
    public interface IUpdateManager
    {
        public void CheckVersion();

        public void CheckStatus();

        public void StopRabotService();

        public void StartRabotService();

        public void RemoveOldRabotDirectory();

        public void DownloadNewRabotVersion();
    }
}
