using OF.Base.Objects;
using OF.Base.ViewModel;
using System.Windows.Threading;

namespace OF.Base.Wpf.UiFunctions
{
    public static class UiFunctions
    {
        #region Properties
        public static Dispatcher Dispatcher { get; set; } = Dispatcher.CurrentDispatcher;
        #endregion

        #region Methods PUBLIC
        public static bool ShowQuestion(string question)
        {
            return ShowDialogWindow(new QuestionViewModel(question), DialogWindowButtonLayout.YesNo, "Question");
        }

        public static void ShowInfoOk(string message)
        {
            ShowDialogWindow(new InfoViewModel(message), DialogWindowButtonLayout.Ok, "Info");
        }

        public static bool ShowInfoOkCancel(string message)
        {
            return ShowDialogWindow(new InfoViewModel(message), DialogWindowButtonLayout.OkCancel, "Info");
        }

        public static void ShowWarningOk(string message)
        {
            ShowDialogWindow(new WarningViewModel(message), DialogWindowButtonLayout.Ok, "Warning");
        }
        public static bool ShowWarningOkCancel(string message)
        {
            return ShowDialogWindow(new WarningViewModel(message), DialogWindowButtonLayout.OkCancel, "Warning");
        }

        public static void ShowError(Exception ex)
        {
            ShowError($"An unexpected error has occurred:{Environment.NewLine}{ex.Message}");
        }

        public static void ShowError(OfException ex)
        {
            ShowError(ex.Message);
        }

        public static void ShowError(string errorMessage)
        {
            ShowDialogWindow(new ErrorViewModel(errorMessage), DialogWindowButtonLayout.Ok, "Error");
        }
        #endregion

        #region Methods PRIVATE
        private static bool ShowDialogWindow(ViewModelBase viewModel, DialogWindowButtonLayout buttons, string title)
        {
            bool isNotCanceledResult = false;
            Action action = () =>
            {
                DialogWindow window = new DialogWindow(viewModel, buttons);
                window.Title = title;


                EventHandler<DialogWindowClosingEventArgs> onCloseHandler = (sender, e) =>
                {
                    switch (e.Button)
                    {
                        case DialogWindowButton.Cancel:
                        case DialogWindowButton.No:
                            isNotCanceledResult = false;
                            break;
                        case DialogWindowButton.Ok:
                        case DialogWindowButton.Yes:
                            isNotCanceledResult = true;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                };
                window.ViewModel.DialogWindowClosing += onCloseHandler;
                window.ShowDialog();
                window.ViewModel.DialogWindowClosing -= onCloseHandler;
            };

            Dispatcher.Invoke(action);
            return isNotCanceledResult;
        }
        #endregion
    }
}
