using Topshelf.HostConfigurators;
using Topshelf.WinformsHost;

namespace Topshelf
{
    public static class HostConfiguratorExtensions
    {
        public static HostConfigurator UseWinformsHost(this HostConfigurator me)
        {
            me.UseHostBuilder((e, s) => new WinformsHostBuilder(e, s));
            return me;
        }
    }
}
