namespace OF.FSimMan.Base
{
    public interface ICustomSingleton<T>
    {
        public static abstract T Instance { get; }
    }
}
