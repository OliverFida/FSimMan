using System.Windows;

namespace OF.FSimMan.Updater
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            string[] debugArgs = {
                "--ghusername=OliverFida",
                "--ghrepo=FSimMan",
                "--currentversion=0.0.1"
            };
            CurrentUpdater.ParseStartArgs(debugArgs);
#else
            CurrentUpdater.ParseStartArgs(e.Args);
#endif
        }
    }
}
