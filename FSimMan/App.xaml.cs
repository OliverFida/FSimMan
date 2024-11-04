﻿using OliverFida.FSimMan.UI;
using System.Diagnostics;
using System.Windows;

namespace FSimMan
{
    public partial class App : Application
    {
        public App()
        {
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
            Current.Shutdown();
        }
    }

}