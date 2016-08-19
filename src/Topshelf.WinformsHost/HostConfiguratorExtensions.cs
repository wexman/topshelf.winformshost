using Topshelf.HostConfigurators;

namespace Topshelf.WinformsHost
{
    public static class HostConfiguratorExtensions
    {
        public static HostConfigurator UseWinformsHost(this HostConfigurator me, bool autostart = true)
        {
            me.UseHostBuilder((e, s) => new WinformsHostBuilder(e, s, autostart));
            return me;
        }
    }
}
