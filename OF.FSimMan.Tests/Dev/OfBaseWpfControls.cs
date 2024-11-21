using OF.Base.Wpf.Testing;
using System.Windows;

namespace OF.FSimMan.Tests.Dev
{
    [TestClass]
    public sealed class OfBaseWpfControls
    {
        [TestMethod]
        public void ShowTestingWindow()
        {
            ShowDialogWindow<TestingWindow>();
        }

        private void ShowDialogWindow<T>() where T : Window
        {
            // The dispatcher thread
            var thread = new Thread(() =>
            {
                T window = Activator.CreateInstance<T>();

                // Initiates the dispatcher thread shutdown when the window closes
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                window.Show();

                // Makes the thread support message pumping
                System.Windows.Threading.Dispatcher.Run();
            });

            // Configure the thread
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
