namespace OliverFida.Base
{
    public interface ISingleton<T>
    {
        static abstract T Instance { get; }
    }
}
