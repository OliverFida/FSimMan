using OF.Base.Client;

namespace OF.Base
{
    internal class Test : ClientBase
    {
        public Test() : base() { }

        public void TestFn()
        {
            IsBusy = true;
        }
    }
}
