﻿using OF.Base.Objects;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Game;
using OF.FSimMan.Management.Games.Fs;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs25ViewModel : FsViewModelBase
    {
        #region Properties
        public override bool IsOpenable => SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs25>().IsFullyConfigured;
        #endregion

        #region Commands
        protected override void NewModPackDelegate()
        {
            try
            {
                ModPack modPack = ((IGameClient)Client).GetNewModPack();
                _editModPackViewModel = new Fs25EditModPackViewModel(Management.EditMode.New, modPack, (Fs25Client)Client);

                _editModPackViewModel.ViewModelClosedEvent += HandleEditModPackViewModelClosedEvent;
                MainViewModel.ViewModelSelector.OpenViewModel(_editModPackViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        protected override void EditModpackDelegate()
        {
            try
            {
                if (EditModpackCommand.Parameter == null) return;
                ModPack modPack = (ModPack)EditModpackCommand.Parameter;

                _editModPackViewModel = new Fs25EditModPackViewModel(Management.EditMode.Edit, modPack, (Fs25Client)Client);

                _editModPackViewModel.ViewModelClosedEvent += HandleEditModPackViewModelClosedEvent;
                MainViewModel.ViewModelSelector.OpenViewModel(_editModPackViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public Fs25ViewModel() : base(new Fs25Client()) { }
        #endregion
    }
}
