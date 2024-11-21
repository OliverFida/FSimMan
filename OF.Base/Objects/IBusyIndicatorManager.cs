namespace OF.Base.Objects
{
    public interface IBusyIndicatorManager : IBindingObject
    {
        public bool IsBusy { get; set; }
        public string BusyContent { get; set; }

        public void ResetBusyIndicator();
    }
}
