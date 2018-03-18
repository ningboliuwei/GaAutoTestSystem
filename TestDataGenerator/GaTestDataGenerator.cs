using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TestDataGenerator
{
    public class GaTestDataGenerator
    {
        public static List<AssertionInfo> GetAssertions(GaParameterInfo gaParameters, AbstractFunction function,
            List<string> targetPaths)
        {
            var assertions = new List<AssertionInfo>();
            // 写文件的准备
            const string logPath = @"c:\#GA_DEMO\galog.txt";
            var writer = new StreamWriter(logPath);
            var stopwatch = new Stopwatch();
            var builder = new StringBuilder();

            stopwatch.Start();
            foreach (var targetPath in targetPaths)
            {
                var found = false;
                // 下面这句漏掉会出错！
                function.TargetPath = targetPath;
                //新建一个种群
                var population = new Population(gaParameters, function);
                //随机生成染色体
                population.RandomGenerateChromosome();

                for (var i = 0; i < gaParameters.GenerationQuantity; i++)
                {
                    var maxFitness = population.Chromosomes.Max(c => c.Fitness);
                    var mostFittest = population.Chromosomes.First(c =>
                        Equals(c.Fitness, maxFitness));
                    stopwatch.Stop();
                    // 得到当前代最优染色体信息
                    var line =
                        $"{$"{i + 1}",-6} | {$"value(s): {string.Join(" ", mostFittest.DecodedSubValues.ToArray())}",-30} | {$"target path: {function.TargetPath}",-30} | {$"execution path: {mostFittest.ExecutionPath}",-30} | {$"fitness: {mostFittest.Fitness}",-10} | {$"result: {mostFittest.Result}",-20}";
                    builder.AppendLine(line);
                    //以下为终止条件
                    if (mostFittest.ExecutionPath == targetPath)
                    {
                        found = true;
                        //将找到的数据添加到测试数据集中
                        var assertion = new AssertionInfo();
                        assertion.InputValues.AddRange(mostFittest.DecodedSubValues.Select(v => v).ToList());
                        assertions.Add(assertion);
                        builder.AppendLine("FOUND".PadLeft(130 / 2, '-').PadRight(130, '-'));
                        break;
                    }
                    //进化
                    population.Evolve(gaParameters.SelectionType);
                    stopwatch.Start();
                }
                stopwatch.Stop();
                if (!found)
                {
                    builder.AppendLine("NOT FOUND".PadLeft(130 / 2, '-').PadRight(130, '-'));
                }

                stopwatch.Start();
            }

            stopwatch.Stop();
            builder.AppendLine($"total time cost: {stopwatch.ElapsedMilliseconds} ms");
            writer.Write(builder.ToString());
            writer.Close();

            return assertions;
        }
    }
}