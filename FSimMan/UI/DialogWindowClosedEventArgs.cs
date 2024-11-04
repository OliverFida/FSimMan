namespace OliverFida.FSimMan.UI
{
    public class DialogWindowClosedEventArgs : EventArgs
    {
        public DialogWindowButton Button { get; }

        public DialogWindowClosedEventArgs(DialogWindowButton button)
        {
            Button = button;
        }
    }
}
