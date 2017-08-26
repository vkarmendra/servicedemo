using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Topshelf;

namespace TopShelfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 //1
            {
                x.Service<LogService>(s =>                        //2
                {
                    s.ConstructUsing(tc => new LogService());
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("Demo log service.");        //7
                x.SetDisplayName("LogService");                       //8
                x.SetServiceName("LogService");                       //9
                x.StartAutomaticallyDelayed();
            });
        }
    }

    public class LogService
    {
        System.Timers.Timer timer;
        ILogger logger;
        public void Start()
        {
            logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            timer = new System.Timers.Timer();
            timer.Elapsed += Log;
            timer.Interval = 3000;
            timer.Start();
        }

        private void Log(object sender, System.Timers.ElapsedEventArgs e)
        {
            logger.Information("This id demo log at {DateTime}", DateTime.Now);
        }

        public void Stop()
        {
            timer?.Dispose();
        }
    }
}
