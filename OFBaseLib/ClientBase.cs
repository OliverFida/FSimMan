namespace OliverFida.Base
{
    public class ClientBase : ObjectBase
    {
        private string _title;
        public string Title
        {
            get => _title;
        }

        public ClientBase(string title)
        {
            _title = title;
        }
    }
}
