using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{
    public class TestSuiteInfo
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public List<TestCaseInfo> TestCases { get; set; } = new List<TestCaseInfo>();
    }
}
