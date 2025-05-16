using Microsoft.Win32;
using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Client.Game.Fs.Modi;
using OF.FSimMan.Game;
using System.Windows;

namespace OF.FSimMan.ViewModel.Game.Fs.Modi
{
    public class ModiE83_Fs25SyncModpackViewModel : BusyViewModelBase
    {
        #region Commands
        public Command CancelCommand { get; }
        private void CancelDelegate()
        {
            try
            {
                ModiE83_Fs25SyncModPackClient client = (ModiE83_Fs25SyncModPackClient)Client;
                if (client.ModPack.IsModified)
                    if (!UiFunctions.ShowQuestion("Exit without saving?")) return;

                Application.Current.Dispatcher.Invoke(client.CancelEdit);
                MainViewModel.ViewModelSelector.CloseCurrentViewModel();
                InvokeViewModelClosedEvent();
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command OkCommand { get; }
        private void OkDelegate()
        {
            try
            {
                ModiE83_Fs25SyncModPackClient client = (ModiE83_Fs25SyncModPackClient)Client;
                client.Store();
                MainViewModel.ViewModelSelector.CloseCurrentViewModel();
                InvokeViewModelClosedEvent();
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command SyncPathCommand { get; }
        private void SyncPathDelegate()
        {
            try
            {
                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Title = "Select Sync Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                ((ModiE83_Fs25SyncModPackClient)Client).ModPack.ModiE83_SyncPath = dialog.FolderName;
            }
            catch(OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public ModiE83_Fs25SyncModpackViewModel(ModPack modPack, Fs25Client gameClient) : base(modPack.Title, new ModiE83_Fs25SyncModPackClient(modPack, gameClient))
        {
            CancelCommand = new Command(this, target => ExecuteBusy(((ModiE83_Fs25SyncModpackViewModel)target).CancelDelegate));
            OkCommand = new Command(this, target => ExecuteBusy(((ModiE83_Fs25SyncModpackViewModel)target).OkDelegate));
            SyncPathCommand = new Command(this, target => ExecuteBusy(((ModiE83_Fs25SyncModpackViewModel)target).SyncPathDelegate));
        }
        #endregion
    }
}
