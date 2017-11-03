﻿using System.Collections.Generic;
using System.Linq;

namespace TestDataGenerator
{
    public class AssertionInfo
    {
        public List<double> InputValues { get; set; }
        public double ExpectedOutput { get; set; }
        public double FactOutput { get; set; }
    }

    public class BoundaryTestHelper
    {
        //得到边界测试数据集
        public static List<List<double>> GetBoundaryTestDataSet(AbstractFunction function)
        {
            var boundaryValuesOfAllParas = function.Paras.Select(GetBoundaryValuesOfPara).ToList();
            var middleValues = boundaryValuesOfAllParas.Select(v => v["middle"]).ToList();
            var lines = new List<List<double>>();

            for (var i = 0; i < boundaryValuesOfAllParas.Count; i++)
            {
                foreach (var value in boundaryValuesOfAllParas[i].Where(v => v.Key != "middle").Select(p => p).ToList())
                {
                    var line = new List<double>();
                    line.AddRange(middleValues);
                    line[i] = value.Value;
                    lines.Add(line);
                }
            }

            return lines;
        }

        //根据参数得到相关的边界值测试数据
        public static Dictionary<string, double> GetBoundaryValuesOfPara(ParaInfo para)
        {
            var step = para.DataType == ParaDataType.Double ? 0.1 : 1;
            var values = new Dictionary<string, double>
            {
                {"lower-", para.LowerBound - step},
                {"lower", para.LowerBound},
                {"lower+", para.LowerBound + step},
                {"middle", (para.LowerBound + para.UpperBound) / 2},
                {"upper-", para.UpperBound - step},
                {"upper", para.UpperBound},
                {"upper+", para.UpperBound + step}
            };

            return values;
        }
    }
}