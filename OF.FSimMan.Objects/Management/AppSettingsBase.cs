using OF.Base.Objects;
using System.Runtime.CompilerServices;

namespace OF.FSimMan.Management
{
    public abstract class AppSettingsBase : BindingObject
    {
        #region Events
        public event EventHandler<AppSettingsStoreTriggerEventArgs>? StoreTrigger;
        #endregion

        #region Methods PROTECTED
        protected override bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            bool hasChanged = base.SetProperty(ref field, value, propertyName);
            if (hasChanged) InvokeSettingsChanged(propertyName);
            return hasChanged;
        }

        protected void InvokeSettingsChanged(AppSettingsStoreTriggerEventArgs e)
        {
            StoreTrigger?.Invoke(this, e);
        }
        #endregion

        #region Methods PRIVATE
        private void InvokeSettingsChanged(string? propertyName)
        {
            InvokeSettingsChanged(new AppSettingsStoreTriggerEventArgs(GetType(), propertyName));
        }
        #endregion
    }
}
