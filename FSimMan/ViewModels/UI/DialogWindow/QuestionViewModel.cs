using OF.Base.ViewModel;

namespace OliverFida.FSimMan.ViewModels.UI.DialogWindow
{
    public class QuestionViewModel : ViewModelBase
    {
        public string Question { get; }

        public QuestionViewModel(string question)
        {
            Question = question;
        }
    }
}
