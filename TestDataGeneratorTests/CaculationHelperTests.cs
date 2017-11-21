using NUnit.Framework;

namespace TestDataGenerator.Tests
{
    [TestFixture]
    public class CaculationHelperTests
    {
        [Test]
        public void CaculatePathMatchFitnessTest()
        {
            Assert.AreEqual(CaculationHelper.CaculatePathMatchFitness("abcdj", "abhj"), 2);
            Assert.AreEqual(CaculationHelper.CaculatePathMatchFitness("abhj", "abhj"), 4);
            Assert.AreEqual(CaculationHelper.CaculatePathMatchFitness("abcegj", "abhj"), 2);
        }
    }
}