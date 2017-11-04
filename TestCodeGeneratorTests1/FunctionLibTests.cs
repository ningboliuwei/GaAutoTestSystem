using NUnit.Framework;

namespace TestCodeGenerator.Tests
{
    [TestFixture]
    public class FunctionLibTests
    {
        [Test]
        public void TriangleTypeTest()
        {
            Assert.AreEqual("invalid value(s)", FunctionLib.TriangleType(0, 1, 2));
        }
    }
}