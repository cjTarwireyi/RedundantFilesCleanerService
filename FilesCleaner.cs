using System.Reflection;
using System.Timers;

namespace RedundantFilesCleanerService
{
    public class FilesCleaner
    {
        private readonly System.Timers.Timer _timer;
        public FilesCleaner()
        {
            //Change this to execute after 24 hours
            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += DeleteFiles;
        }
        private void DeleteFiles(object sender, ElapsedEventArgs e)
        { 
            //TODO: add configuration which allow the user to between deleting files including sub folders or only files
            var exPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var path = exPath + "\\directoryConfig.txt";
          
           if(File.Exists(path))
            {
                string[] directoryPathList = File.ReadAllLines(path);
                if(directoryPathList.Length > 0)
                {
                    var userCustomDaysStr = directoryPathList.Length > 1 ? directoryPathList[0] : string.Empty;
                    directoryPathList = directoryPathList.Length > 1 ? directoryPathList.Skip(1).ToArray() : directoryPathList;                   
                    
                    foreach (string directoryPath in directoryPathList)
                    {

                        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                        if(directoryInfo.Exists)
                        {
                            var files = directoryInfo.EnumerateFiles();
                            var directories = directoryInfo.EnumerateDirectories();
                            var olderThanDays = -355;

                            if (!string.IsNullOrWhiteSpace(userCustomDaysStr) && int.TryParse(userCustomDaysStr, out int userCustomDays))
                            {
                                olderThanDays = userCustomDays > 0 ? 0 - userCustomDays : userCustomDays;
                            }
                            if (olderThanDays < 0)
                            {
                                foreach (var file in files.Where(e => e.LastWriteTime < DateTime.Now.AddDays(olderThanDays) && e.CreationTime < DateTime.Now.AddDays(olderThanDays)))
                                {
                                    if (file.Exists)
                                    {
                                        file.Delete();
                                    }
                                }
                                foreach (var directory in directories.Where(e => e.LastWriteTime < DateTime.Now.AddDays(olderThanDays) && e.CreationTime < DateTime.Now.AddDays(olderThanDays)))
                                {
                                    if (directory.Exists)
                                    {
                                        directory.Delete(true);
                                    }
                                }
                            }
                           
                        }
                      
                    }
                }
               
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
