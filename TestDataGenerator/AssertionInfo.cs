using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{
    public class AssertionInfo
    {
        public List<double> InputValues { get; set; } = new List<double>();
        public object ExpectedOutput { get; set; }
        public object FactOutput { get; set; }
    }
}
