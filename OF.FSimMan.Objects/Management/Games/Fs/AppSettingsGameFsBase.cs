﻿namespace OF.FSimMan.Management.Games.Fs
{
    public abstract class AppSettingsGameFsBase : AppSettingsGameBase
    {
        #region Properties

        internal string _dataDirectoryPath = string.Empty;
        public string DataDirectoryPath
        {
            get => _dataDirectoryPath;
            set { if (SetProperty(ref _dataDirectoryPath, value)) InvokeSettingsChanged(); }
        }

        public override bool IsFullyConfigured
        {
            get
            {
                if (string.IsNullOrEmpty(DataDirectoryPath)) return false;

                return base.IsFullyConfigured;
            }
        }
        #endregion
    }
}
