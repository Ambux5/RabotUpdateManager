namespace RabotUpdateManager.Options
{
    public class EnviromentOptions
    {
        public const string SectionName = "Enviroment";

        public string Key { get; set; }

        public string ServiceName { get; set; }

        public string ArtifactUrl { get; set; }

        public string Version { get; set; }

        public string FileName { get; set; }

        public string serviceWorkingDirectory { get; set; }
    }
}
