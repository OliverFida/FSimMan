using Microsoft.Win32;
using CLS.Core.Client;
using CLS.Core.ViewModel;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Game;
using OF.FSimMan.Management;
using System.IO;
using System.Windows;
using CLS.Core.ViewModel.Command;
using CLS.Core;
using CLS.Core.Wpf.UiFunctions;

namespace OF.FSimMan.ViewModel.Game
{
    public abstract class EditModPackViewModelBase : BusyViewModelBase
    {
        #region Properties
        private EditMode _editMode;
        #endregion

        #region Commands
        public Command ExitCommand { get; }
        private Task ExitDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    EditModPackClientBase client = (EditModPackClientBase)Client;
                    if (client.ModPack.IsModified || _editMode == EditMode.New)
                    {
                        if (!UiFunctions.ShowQuestion("Exit without saving?")) return;
                    }
                    Application.Current.Dispatcher.Invoke(client.CancelEdit);
                    MainViewModel.ViewModelSelector.CloseCurrentViewModel();
                    InvokeViewModelClosedEvent();
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command SaveCommand { get; }
        private Task SaveDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    ((EditModPackClientBase)Client).StoreModPack();
                    _editMode = EditMode.Edit;
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command AddModCommand { get; }
        private Task AddModDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    OpenFileDialog fileDialog = new OpenFileDialog()
                    {
                        InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
                        Filter = "zip archives (*.zip)|*.zip",
                        Title = "Select mod .zip",
                        DefaultExt = "zip",
                        Multiselect = true
                    };
                    bool? result = fileDialog.ShowDialog();
                    if (result != true) return;

                    Application.Current.Dispatcher.Invoke(() => ((EditModPackClientBase)Client).AddMods(fileDialog.FileNames));
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command RemoveModCommand { get; }
        private Task RemoveModDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    if (RemoveModCommand.Parameter == null) return;
                    Mod mod = (Mod)RemoveModCommand.Parameter;

                    if (!UiFunctions.ShowQuestion($@"Are you sure you want to remove the mod ""{mod.Title}""?")) return;

                    Application.Current.Dispatcher.Invoke(() => ((EditModPackClientBase)Client).RemoveMod(mod));
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command SelectImageCommand { get; }
        private Task SelectImageDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    OpenFileDialog fileDialog = new OpenFileDialog()
                    {
                        InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
                        Filter = "images|*.png;*.jpg;*.jpeg",
                        Title = "Select image",
                        DefaultExt = "png",
                        Multiselect = false
                    };
                    bool? result = fileDialog.ShowDialog();
                    if (result != true) return;

                    Application.Current.Dispatcher.Invoke(() => ((EditModPackClientBase)Client).AddIcon(fileDialog.FileName));
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command RemoveImageCommand { get; }
        private Task RemoveImageDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    if (!UiFunctions.ShowQuestion($@"Are you sure you want to remove the image?")) return;

                    Application.Current.Dispatcher.Invoke(() => ((EditModPackClientBase)Client).RemoveIcon());
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }
        public bool IsRemoveImageEnabled
        {
            get => ((EditModPackClientBase)Client).ModPack.ImageSource is not null;
        }
        #endregion

        #region Constructor
        public EditModPackViewModelBase(string title, IClient client, EditMode editMode) : base(title, client)
        {
            _editMode = editMode;

            ExitCommand = new Command(this, target => ExecuteDelegate(((EditModPackViewModelBase)target).ExitDelegate, false));
            SaveCommand = new Command(this, target => ExecuteDelegate(((EditModPackViewModelBase)target).SaveDelegate, false));
            AddModCommand = new Command(this, target => ExecuteDelegate(((EditModPackViewModelBase)target).AddModDelegate, false));
            RemoveModCommand = new Command(this, target => ExecuteDelegate(((EditModPackViewModelBase)target).RemoveModDelegate, false));
            SelectImageCommand = new Command(this, target => ExecuteDelegate(((EditModPackViewModelBase)target).SelectImageDelegate, false));
            RemoveImageCommand = new Command(this, target => ExecuteDelegate(((EditModPackViewModelBase)target).RemoveImageDelegate, false));

            ((EditModPackClientBase)Client).ModPack.PropertyChanged += ModPack_PropertyChanged;
        }

        private void ModPack_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is not null && e.PropertyName.Equals(nameof(ModPack.ImageSource))) OnPropertyChanged(nameof(IsRemoveImageEnabled));
        }
        #endregion
    }
}
