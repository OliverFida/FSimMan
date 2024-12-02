namespace OF.Base.Wpf.UiFunctions
{
    public class DialogWindowClosingEventArgs : EventArgs
    {
        public DialogWindowButton Button { get; }

        public DialogWindowClosingEventArgs(DialogWindowButton button)
        {
            Button = button;
        }
    }
}
