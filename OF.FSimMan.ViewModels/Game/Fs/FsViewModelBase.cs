﻿using OF.FSimMan.Client.Game.Fs;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public abstract class FsViewModelBase : GameViewModelBase
    {
        #region Properties
        public static bool IsModPackHubVisible { get => ReleaseFeatures.GiantsModPackHub; }
        #endregion

        #region Constructor
        public FsViewModelBase(FsClientBase client) : base(client) { }
        #endregion
    }
}
