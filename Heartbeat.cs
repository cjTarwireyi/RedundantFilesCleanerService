using System.Timers;


namespace WindowsServiceApp
{
    public class Heartbeat
    {
        private readonly System.Timers.Timer _timer;
        public Heartbeat()
        {
            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            string[] lines = new string[] { DateTime.Now.ToString() };
            File.AppendAllLines(@"C:\projects\DocsHeartbeat.txt", lines);
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
