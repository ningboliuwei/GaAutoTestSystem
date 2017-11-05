using NUnit.Framework;
namespace TestCodeGenerator
{
	[TestFixture]
	public class FunctionLibTests
	{
		[Test]
		public void NextDateTest()
		{
			Assert.AreEqual("", FunctionLib.NextDate(1990, 12, 31));
			Assert.AreEqual("", FunctionLib.NextDate(1992, 12, 15));
			Assert.AreEqual("", FunctionLib.NextDate(1960, 10, 31));
			Assert.AreEqual("", FunctionLib.NextDate(1949, 6, 16));
			Assert.AreEqual("", FunctionLib.NextDate(1950, 6, 16));
			Assert.AreEqual("", FunctionLib.NextDate(1951, 6, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2049, 6, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2050, 6, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2051, 6, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 0, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 1, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 2, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 11, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 12, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 13, 16));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 6, 0));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 6, 1));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 6, 2));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 6, 30));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 6, 31));
			Assert.AreEqual("", FunctionLib.NextDate(2000, 6, 32));
		}
	}
}
