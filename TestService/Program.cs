using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.WinformsHost;

namespace TestService
{
    class Program
    {
        static int Main(string[] args)
        {
            HostFactory.Run(cc =>
            {
                cc.UseWinformsHost();
                cc.RunAsNetworkService();
                cc.StartAutomaticallyDelayed();
                cc.SetDisplayName("Sensaction Sensor Service");
                cc.SetDescription("Sensaction Sensor Service");
                cc.SetServiceName("Sensaction.SensorService");

                cc.Service<SomeService>();
            });
            return (int)TopshelfExitCode.Ok;
        }
    }
}
