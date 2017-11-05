using System.Collections.Generic;

namespace TestDataGenerator
{
    public class AssertionInfo
    {
        public List<double> InputValues { get; set; } = new List<double>();
        public object ExpectedOutput { get; set; }
        public object ActualOutput { get; set; }
    }
}