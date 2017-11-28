﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TestDataGenerator
{
    public class RandomTestDataGenerator
    {
        public static List<AssertionInfo> GetAssertions(GaParameterInfo gaParameters, AbstractFunction function,
            List<string> targetPaths)
        {
            var rnd = new Random();
            var builder = new StringBuilder();
            var values = new double[function.Paras.Count];
            var assertions = new List<AssertionInfo>();
            const string logPath = @"c:\#GA_DEMO\rndlog.txt";
            var writer = new StreamWriter(logPath);
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            foreach (var targetPath in targetPaths)
            {
                // 下面这句漏掉会出错！
                function.TargetPath = targetPath;
                for (long i = 0; i < gaParameters.ChromosomeQuantity * gaParameters.GenerationQuantity; i++)
                {
                    // 随机生成所有实参的值
                    foreach (var para in function.Paras)
                    {
                        values[function.Paras.IndexOf(para)] =
                            (int) (rnd.NextDouble() * (para.UpperBound - para.LowerBound) + para.LowerBound);
                    }

                    var result = function.OriginalFunction(values);
                    var executionPath = function.StubbedFunction(values);
                    // 输出信息部分不计时
                    stopwatch.Stop();
                    //每个信息占的宽度
                    const int width = -30;
                    var line =
                        $"{$"value(s): {string.Join(" ", values.ToArray())}",width} | {$"target path: {function.TargetPath}",width} | {$"execution path: {executionPath}",width} | {$"result: {result}",width}";
                    builder.AppendLine(line);
                   
                    //以下为终止条件
                    //如果当前执行路径包含任何目标路径
                    if (executionPath.Contains(targetPath))
                    {
                        //将找到的数据添加到测试数据集中
                        var assertion = new AssertionInfo();
                        assertion.InputValues.AddRange(values.ToList());
                        assertions.Add(assertion);
                        builder.AppendLine("FOUND".PadLeft((line.Length - 5) / 2, '-').PadRight(line.Length, '-'));
                        break;
                    }

                    stopwatch.Start();
                }
            }

            stopwatch.Stop();
            builder.AppendLine($"total time cost: {stopwatch.ElapsedMilliseconds} ms");
            writer.Write(builder.ToString());
            writer.Close();

            return assertions;
        }
    }
}