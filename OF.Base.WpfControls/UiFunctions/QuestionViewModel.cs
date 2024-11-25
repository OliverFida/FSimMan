using OF.Base.ViewModel;

namespace OF.Base.Wpf.UiFunctions
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
