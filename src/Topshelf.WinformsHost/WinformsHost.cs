using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Topshelf.Logging;
using Topshelf.Runtime;

namespace Topshelf.WinformsHost
{
    public class WinformsRunHost : Host, HostControl
    {
        private readonly HostSettings _settings;
        private readonly ServiceHandle _serviceHandle;
        private readonly HostEnvironment _environment;

        private readonly LogWriter _log = HostLogger.Get<WinformsRunHost>();

        private bool _Autostart;
        private int _IsRunning = 0;

        public WinformsRunHost(HostEnvironment environment, HostSettings settings, ServiceHandle serviceHandle, bool autostart)
        {
            _settings = settings;
            _Autostart = autostart;
            _environment = environment;
            _serviceHandle = serviceHandle;
        }

        public TopshelfExitCode Run()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CatchUnhandledException);
            if (this._environment.IsServiceInstalled(this._settings.ServiceName) && !this._environment.IsServiceStopped(this._settings.ServiceName))
            {
                this._log.ErrorFormat("The {0} service is running and must be stopped before running as winforms application", new object[]
                {
                    this._settings.ServiceName
                });
                return TopshelfExitCode.ServiceAlreadyRunning;
            }

            try
            {
                this._log.Debug("Starting up as a winforms application");

                var form = new Form();
                form.Text = this._settings.DisplayName;

                var btn = new Button();

                if (_Autostart)
                    Start();

                btn.Click += (s, e) =>
                {
                    if (_IsRunning == 1)
                    {
                        Stop();
                        btn.Text = "Start";
                    }
                    else
                    {
                        Start();
                        btn.Text = "Stop";
                    }
                };
                btn.Text = _IsRunning == 1 ? "Stop" : "Start";
                btn.Dock = DockStyle.Fill;
                btn.Parent = form;

                Application.Run(form);
                return TopshelfExitCode.Ok;
            }
            catch (Exception exception)
            {
                this._log.Error("An exception occurred", exception);
                return TopshelfExitCode.AbnormalExit;
            }
            finally
            {
                Stop();
                HostLogger.Shutdown();
            }
        }

        public void RequestAdditionalTime(TimeSpan timeRemaining)
        {
            this._log.Info("Ignoring request for additional time because we don't support that");
        }

        public void Restart()
        {
            this._log.Info("Service Restart requested, but we don't support that here, so we are exiting.");
            this.Stop();
        }

        private void Start()
        {
            if (!this._serviceHandle.Start(this))
            {
                throw new TopshelfException("The service failed to start (return false).");
            }
            _IsRunning = 1;
        }

        public void Stop()
        {
            var running = Interlocked.Exchange(ref _IsRunning, 0);
            if (running == 1)
            {
                this._log.Info("Service Stop requested, exiting.");
                _serviceHandle.Stop(this);
            }
            Application.ExitThread();
        }
    }
}
