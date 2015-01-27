using Topshelf.HostConfigurators;

namespace Topshelf.WinformsHost
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
