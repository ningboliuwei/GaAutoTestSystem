﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{
    public class RandomTestDataGenerator
    {
        public static List<AssertionInfo> GetAssertions(GaParameterInfo gaParameters, AbstractFunction function,
            List<string> targetPaths)
        {
            var rnd = new Random();
//            var builder = new StringBuilder();
            var paras = function.Paras;
            var assertions = new List<AssertionInfo>();

            foreach (var targetPath in targetPaths)
            {
                for (long i = 0; i < gaParameters.GenerationQuantity; i++)
                {
                    foreach (var para in paras)
                    {
                        para.Value = rnd.NextDouble() * (para.UpperBound - para.LowerBound) + para.LowerBound;
                    }

                    var result = function.Result;

//                    builder.Clear();
//                    builder.Append($"after {i + 1:0000} time(s): timecost: {stopwatch.ElapsedMilliseconds} ms");
//                    builder.Append(
//                        $" | value: {string.Join(" ", paras.Select(p => p.Value).ToArray())} | fitness: {fitness} | execution path: {executionPath} | result: {result}");
//                    builder.Append(Environment.NewLine);
//                    txtResult.AppendText(builder.ToString());
//                    txtResult.ScrollToCaret();

                    //以下为终止条件
                    //如果当前执行路径包含任何目标路径
                    if (function.ExecutionPath.Contains(targetPath))
                    {
                        //将找到的数据添加到测试数据集中
                        var assertion = new AssertionInfo();
                        assertion.InputValues.AddRange(function.Paras.Select(p=>p.Value).ToList());
                        assertions.Add(assertion);

                        break;
                    }
                }
            }

            return assertions;
        }
    }
}
