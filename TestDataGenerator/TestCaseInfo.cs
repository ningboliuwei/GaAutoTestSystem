using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{
    public class TestCaseInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<AssertionInfo> Assertions { get; set; } = new List<AssertionInfo>();
    }
}
