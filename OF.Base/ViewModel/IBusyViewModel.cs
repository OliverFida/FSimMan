using OF.Base.Client;
using OF.Base.Objects;

namespace OF.Base.ViewModel
{
    public interface IBusyViewModel : IViewModel, IBusyIndicatorManager
    {
        public IClient Client { get; }

        public void ExecuteBusy(Action action);
    }
}
