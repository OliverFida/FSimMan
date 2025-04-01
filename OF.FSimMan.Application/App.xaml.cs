using OF.Base.Wpf.UiFunctions;
using System.Diagnostics;
using System.Windows;

namespace OF.FSimMan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructor
        public App()
        {
            UiFunctions.Dispatcher = Dispatcher;

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            HandleException(args.ExceptionObject as Exception, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (sender, args) =>
            {
                HandleException(args.Exception, "Application.DispatcherUnhandledException");
                args.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                HandleException(args.Exception, "TaskScheduler.UnobservedTaskException");
                args.SetObserved();
            };
        }
        #endregion

        #region Methods PROTECTED
        protected override void OnStartup(StartupEventArgs e)
        {
            if (CheckIsAlreadyRunning())
            {
                Shutdown();
                return;
            }

#if DEBUG
            if (e.Args.Contains("--manual-testing")) CurrentApplication.LaunchMode = LaunchMode.ManualTesting;
#endif

            base.OnStartup(e);
        }
        #endregion

        #region Methods PRIVATE
        private bool CheckIsAlreadyRunning()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            int processCount = Process.GetProcessesByName(processName).Count();
            if (processCount > 1) return true;

            return false;
        }

        private void HandleException(Exception? exception, string source)
        {
            if (exception == null) return;

            UiFunctions.ShowError(exception);

#if !DEBUG
            if (UiFunctions.ShowQuestion("Would you like to restart the application?")) RestartApplication();
#endif
        }

        private void RestartApplication()
        {
            bool tryStart = false;
            ProcessModule? processModule = Process.GetCurrentProcess().MainModule;
            if (processModule != null) tryStart = true;

            if (tryStart) Process.Start(processModule!.FileName);
            Current.Shutdown(0);
        }
        #endregion
    }
}
