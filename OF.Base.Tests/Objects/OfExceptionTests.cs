using OF.Base.Objects;
using System.Diagnostics.CodeAnalysis;

namespace OF.Base.Tests.Objects
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class OfExceptionTests
    {
        [TestMethod]
        public void OfException_Self()
        {
            OfException ex = new OfException();
            Assert.AreEqual("Exception of type 'OF.Base.Objects.OfException' was thrown.", ex.Message);
            Assert.AreEqual(null, ex.InnerException);

            string exceptionMessage = "Test exception";
            ex = new OfException(exceptionMessage);
            Assert.AreEqual(exceptionMessage, ex.Message);
            Assert.AreEqual(null, ex.InnerException);

            InvalidOperationException iex = new InvalidOperationException();
            ex = new OfException(exceptionMessage, iex);
            Assert.AreEqual(exceptionMessage, ex.Message);
            Assert.AreEqual(iex, ex.InnerException);
        }
    }
}
