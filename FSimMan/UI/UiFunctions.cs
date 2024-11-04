using OliverFida.Base;
using OliverFida.FSimMan.ViewModels.UI.DialogWindow;

namespace OliverFida.FSimMan.UI
{
    internal static class UiFunctions
    {
        internal static bool ShowQuestion(string question)
        {
            return ShowDialogWindow(new QuestionViewModel(question), DialogWindowButtonLayout.YesNo, "Question");
        }

        internal static void ShowInfoOk(string message)
        {
            ShowDialogWindow(new InfoViewModel(message), DialogWindowButtonLayout.Ok, "Info");
        }
        internal static bool ShowInfoOkCancel(string message)
        {
            return ShowDialogWindow(new InfoViewModel(message), DialogWindowButtonLayout.OkCancel, "Info");
        }

        internal static void ShowWarningOk(string message)
        {
            ShowDialogWindow(new WarningViewModel(message), DialogWindowButtonLayout.Ok, "Warning");
        }
        internal static bool ShowWarningOkCancel(string message)
        {
            return ShowDialogWindow(new WarningViewModel(message), DialogWindowButtonLayout.OkCancel, "Warning");
        }

        internal static void ShowError(Exception ex)
        {
            ShowError($"An unexpected error has occurred:{Environment.NewLine}{ex.Message}");
        }

        internal static void ShowError(OFException ex)
        {
            ShowError(ex.Message);
        }

        internal static void ShowError(string errorMessage)
        {
            ShowDialogWindow(new ErrorViewModel(errorMessage), DialogWindowButtonLayout.Ok, "Error");
        }

        private static bool ShowDialogWindow(ViewModelBase viewModel, DialogWindowButtonLayout buttons, string title)
        {
            DialogWindow window = new DialogWindow(viewModel, buttons);
            window.Title = $"{CurrentApplication.AppTitleBase} - {title}";


            bool isNotCanceledResult = false;
            EventHandler<DialogWindowClosedEventArgs> onCloseHandler = (object? sender, DialogWindowClosedEventArgs e) =>
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
            window.DialogWindowClosed += onCloseHandler;
            window.ShowDialog();
            window.DialogWindowClosed -= onCloseHandler;

            return isNotCanceledResult;
        }
    }
}
