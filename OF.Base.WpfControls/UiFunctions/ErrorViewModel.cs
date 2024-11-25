using OF.Base.ViewModel;

namespace OF.Base.Wpf.UiFunctions
{
    public class ErrorViewModel : ViewModelBase
    {
        public bool ShowDetails { get; }
        public string ErrorMessage { get; }

        public ErrorViewModel(string errorMessage) : this(errorMessage, false) { }

        public ErrorViewModel(string errorMessage, bool showDetails)
        {
            ShowDetails = showDetails;
            ErrorMessage = errorMessage;
        }
    }
}
