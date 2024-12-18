using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests
{
    [ExcludeFromCodeCoverage]
    public abstract class FSimManTestsBase
    {
        public FSimManTestsBase()
        {
            CurrentApplication.LaunchMode = LaunchMode.UnitTests;
        }
    }
}
