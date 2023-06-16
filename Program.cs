// See https://aka.ms/new-console-template for more information
using Topshelf;
using WindowsServiceApp;

Console.WriteLine("Hello, World!");
var exitCode = HostFactory.Run(x =>
{
    x.Service<Heartbeat>(s =>
    {
        s.ConstructUsing(heartbeat => new Heartbeat());
        s.WhenStarted(heartbeat=> heartbeat.Start());
        s.WhenStopped(heartbeat=> heartbeat.Stop());
    });

    x.RunAsLocalSystem();

    x.SetServiceName("HeartbeatService");
    x.SetDisplayName("Heartbeat Service");
    x.SetDescription("This is a sample heartbeat service.");
});

int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.ExitCode= exitCodeValue;