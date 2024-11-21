namespace OF.Base.Objects
{
    public interface ISingleton<T>
    {
        public static abstract T Instance { get; }
    }
}
