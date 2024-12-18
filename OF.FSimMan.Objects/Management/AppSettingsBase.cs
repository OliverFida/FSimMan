using OF.Base.Objects;

namespace OF.FSimMan.Management
{
    public abstract class AppSettingsBase : BindingObject
    {
        #region Events
        public event EventHandler? StoreTrigger;
        #endregion

        #region Methods PROTECTED
        protected virtual void InvokeSettingsChanged()
        {
            StoreTrigger?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
