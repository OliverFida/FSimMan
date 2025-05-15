using OF.FSimMan.Client.Management;
using OF.FSimMan.Management.Modi;
using OF.FSimMan.ViewModel;
using OF.FSimMan.ViewModel.Game.Fs;
using System.Windows;
using System.Windows.Controls;

namespace OF.FSimMan.View.Style
{
    public class ModpacksDataGridControlsDataTemplateSelector : DataTemplateSelector
    {
        private ModificationKey ModiKey
        {
            get => SettingsClient.Instance.AppSettings.SelectedModiKey;
        }
        public required DataTemplate DefaultTemplate { get; set; }
        public required DataTemplate ModiE83Template { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // FS25
            if (MainViewModel.ViewModelSelector.CurrentViewModel is Fs25ViewModel)
            {
                if (ModiKey.Equals(ModificationKey.E83)) return ModiE83Template;
            }

            return DefaultTemplate;
        }
    }
}
