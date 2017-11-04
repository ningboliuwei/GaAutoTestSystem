using System.Collections.Generic;

namespace TestDataGenerator
{
    public class TestCaseInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<AssertionInfo> Assertions { get; set; } = new List<AssertionInfo>();
    }
}