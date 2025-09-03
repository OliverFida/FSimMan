using System.ComponentModel;

namespace OF.FSimMan.Management
{
    public class AppSettingsStoreTriggerEventArgs : PropertyChangedEventArgs
    {
        public Type Type { get; }

        public AppSettingsStoreTriggerEventArgs(Type type, string? propertyName) : base(propertyName)
        {
            Type = type;
        }
    }
}
