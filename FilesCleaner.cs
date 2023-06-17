using System.Timers;

namespace RedundantFilesCleanerService
{
    public class FilesCleaner
    {
        private readonly System.Timers.Timer _timer;
        public FilesCleaner()
        {
            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += voidDeleteFiles;
        }
        private void voidDeleteFiles(object sender, ElapsedEventArgs e)
        {

            DirectoryInfo directoryInfo = new DirectoryInfo("C:\\WebReports\\Estimate Template");
            var files = directoryInfo.EnumerateFiles();

            foreach (var file in files)
            {
                file.Delete();
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
