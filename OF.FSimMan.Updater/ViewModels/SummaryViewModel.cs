using CLS.Core.ViewModel;
using CLS.Core.ViewModel.Command;
using OF.FSimMan.Updater.Clients;
using System.Windows;

namespace OF.FSimMan.Updater.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {
        #region Properties
        public static UpdateClient UpdateClient
        {
            get => UpdateClient.Instance;
        }

        public string StatusText
        {
            get
            {
                if (UpdateClient.IsUpdateAvailable) return $"Version {UpdateClient.LatestRelease!.TagName} available";
                return "Newest version is installed";
            }
        }

        public string InstallOrCancelButtonText
        {
            get
            {
                if (UpdateClient.IsUpdateAvailable) return "Install";
                return "Cancel";
            }
        }
        #endregion

        #region Commands
        public Command InstallOrCancelCommand { get; }
        private async Task InstallOrCancelDelegate()
        {
            if (UpdateClient.IsUpdateAvailable)
            {
                await UpdateClient.Instance.TryExecuteUpdateAsync();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
        #endregion

        #region Constructor
        public SummaryViewModel() : base("Summary")
        {
            InstallOrCancelCommand = new Command(async () => await ExecuteDelegate(InstallOrCancelDelegate));

            UpdateClient.PropertyChanged += UpdateClient_PropertyChanged;
        }

        private void UpdateClient_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.PropertyName)) return;

            if (e.PropertyName.Equals(nameof(UpdateClient.IsUpdateAvailable)))
            {
                OnPropertyChanged(nameof(StatusText));
                OnPropertyChanged(nameof(InstallOrCancelButtonText));
            }
        }
        #endregion
    }
}
