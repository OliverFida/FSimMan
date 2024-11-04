namespace OliverFida.Base
{
    public interface IClientBase : IObjectBase
    {
        bool IsBusy { get; set; }
        string BusyContent { get; set; }

        void ResetBusyIndicator();
    }
}
