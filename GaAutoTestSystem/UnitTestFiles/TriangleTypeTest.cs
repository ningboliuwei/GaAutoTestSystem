using NUnit.Framework;
namespace TestCodeGenerator
{
	[TestFixture]
	public class FunctionLibTests
	{
		[Test]
		public void TriangleTypeTest()
		{
			Assert.AreEqual("not a triangle", FunctionLib.TriangleType(2, 6, 2));
			Assert.AreEqual("equilateral triangle", FunctionLib.TriangleType(1, 1, 1));
			Assert.AreEqual("isosceles triangle", FunctionLib.TriangleType(6, 6, 2));
			Assert.AreEqual("invalid value(s)", FunctionLib.TriangleType(0, 5, 5));
			Assert.AreEqual("isosceles triangle", FunctionLib.TriangleType(1, 5, 5));
			Assert.AreEqual("isosceles triangle", FunctionLib.TriangleType(2, 5, 5));
			Assert.AreEqual("isosceles triangle", FunctionLib.TriangleType(9, 5, 5));
			Assert.AreEqual("not a triangle", FunctionLib.TriangleType(10, 5, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(11, 5, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 0, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 1, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 2, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 9, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 10, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 11, 5));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 5, 0));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 5, 1));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 5, 2));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 5, 9));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 5, 10));
			Assert.AreEqual("", FunctionLib.TriangleType(5, 5, 11));
		}
	}
}
