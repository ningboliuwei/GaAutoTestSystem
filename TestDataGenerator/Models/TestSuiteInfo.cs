using System.Collections.Generic;

namespace TestDataGenerator
{
    public class TestSuiteInfo
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public List<TestCaseInfo> TestCases { get; set; } = new List<TestCaseInfo>();
    }
}