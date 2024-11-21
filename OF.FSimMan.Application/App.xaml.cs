using System.Diagnostics;
using System.Windows;

namespace OF.FSimMan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // OFDO
        }

        private void HandleException(Exception? exception, string source)
        {
            // OFDO
        }

        private void RestartApplication()
        {
            bool tryStart = false;
            ProcessModule? processModule = Process.GetCurrentProcess().MainModule;
            if (processModule != null) tryStart = true;

            if (tryStart) Process.Start(processModule!.FileName);
            Current.Shutdown(0);
        }
    }
}
