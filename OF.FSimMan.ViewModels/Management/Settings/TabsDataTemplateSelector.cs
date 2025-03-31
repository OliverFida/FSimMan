using System.Windows;
using System.Windows.Controls;

namespace OF.FSimMan.ViewModel.Management.Settings
{
    public class TabsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? SettingsTabsApplicationDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Type type = item.GetType();

            if (type.IsAssignableTo(typeof(ApplicationViewModel))) return SettingsTabsApplicationDataTemplate!;
            return base.SelectTemplate(item, container);
        }
    }
}
