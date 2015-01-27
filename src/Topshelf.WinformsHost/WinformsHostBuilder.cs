using System;
using Topshelf.Builders;
using Topshelf.Runtime;

namespace Topshelf.WinformsHost
{
    public class WinformsHostBuilder : HostBuilder
    {
        private readonly HostEnvironment _Environment;

        private readonly HostSettings _Settings;

        public WinformsHostBuilder(HostEnvironment environment, HostSettings settings)
        {
            this._Environment = environment;
            this._Settings = settings;
        }

        public Host Build(ServiceBuilder serviceBuilder)
        {
            ServiceHandle serviceHandle = serviceBuilder.Build(this._Settings);
            return this.CreateHost(serviceHandle);
        }

        private Host CreateHost(ServiceHandle serviceHandle)
        {
            if (this._Environment.IsRunningAsAService)
            {
                //RunBuilder._log.Debug("Running as a service, creating service host.");
                return this._Environment.CreateServiceHost(this._Settings, serviceHandle);
            }
            //RunBuilder._log.Debug("Running as a console application, creating the console host.");
            return new WinformsRunHost(this._Environment, this._Settings, serviceHandle);
        }

        public void Match<T>(Action<T> callback) where T : class, HostBuilder
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }
            T t = this as T;
            if (t != null)
            {
                callback(t);
            }
        }

        public Topshelf.Runtime.HostEnvironment Environment
        {
            get { return _Environment; }
        }

        public Topshelf.Runtime.HostSettings Settings
        {
            get { return _Settings; }
        }
    }

}
