using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{
    public class AssertionInfo
    {
        public List<double> InputValues { get; set; }
        public double ExpectedOutput { get; set; }
        public double FactOutput { get; set; }
    }

    public class BoundTestHelper
    {
        //得到函数中每个参数的测试数据
        public static List<double[]> GetBoundTestData(AbstractFunction function)
        {
            var paras = function.Paras;
          
            var boundValuesOfAllParas = paras.Select(GetBoundTestValuesOfPara).ToList();
            var r = new List<double>();
            foreach (var boundValues in boundValuesOfAllParas)
            {
                var paraIndex = boundValuesOfAllParas.IndexOf(boundValues);
              
                    var q = boundValuesOfAllParas.Where(v => boundValues.ToList().IndexOf(v) != paraIndex)
                        .Select(v => v["middle"]);

            }
            return null;

        }
        //根据参数得到相关的边界值测试数据
        public static Dictionary<string, double> GetBoundTestValuesOfPara(ParaInfo para)
        {
            var values = new Dictionary<string, double>
            {
                {"lower-", para.LowerBound - 0.1},
                {"lower", para.LowerBound},
                {"lower+", para.LowerBound + 0.1},
                {"middle", (para.LowerBound + para.UpperBound) / 2},
                {"upper-", para.UpperBound - 0.1},
                {"upper", para.UpperBound},
                {"upper+", para.UpperBound + 0.1}
            };

            return values;
        }
    }
}
