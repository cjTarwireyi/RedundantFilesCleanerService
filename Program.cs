using RedundantFilesCleanerService;
using Topshelf;

var exitCode = HostFactory.Run(x =>
{
    x.Service<FilesCleaner>(s =>
    {
        s.ConstructUsing(fileCleaner => new FilesCleaner());
        s.WhenStarted(fileCleaner => fileCleaner.Start());
        s.WhenStopped(fileCleaner => fileCleaner.Stop());
    });
    x.RunAsLocalSystem();

    x.SetServiceName("RedundantFilesCleanerService");
    x.SetDisplayName("Redundant Files Cleaner Service");
    x.SetDescription("This service delete redundant files from user specified folders on this machine");
});

int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.ExitCode = exitCodeValue;