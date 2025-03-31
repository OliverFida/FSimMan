using System.ComponentModel;

namespace OF.FSimMan.Management
{
    public class AppSettingsStoreTriggerEventArgs : PropertyChangedEventArgs
    {
        public AppSettingsStoreTriggerEventArgs(string? propertyName) : base(propertyName) { }
    }
}
