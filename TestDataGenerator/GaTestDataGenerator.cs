using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestDataGenerator
{
    public class GaTestDataGenerator
    {
        public static List<AssertionInfo> GetAssertions(GaParameterInfo gaParameters, AbstractFunction function,
            List<string> targetPaths)
        {
            //新建一个种群
            var population = new Population(gaParameters, function);
            var assertions = new List<AssertionInfo>();
            // 写文件的准备
            const string logPath = @"c:\#GA_DEMO\log.txt";
            var writer = new StreamWriter(logPath);
            //随机生成染色体
            population.RandomGenerateChromosome();

            foreach (var targetPath in targetPaths)
            {
                for (var i = 0; i < gaParameters.GenerationQuantity; i++)
                {
                    var mostFittest = population.Chromosomes.First(c =>
                        Equals(c.Fitness, population.Chromosomes.Max(n => n.Fitness)));
                    // 将当前信息写入文件
                    var line =
                        $"value(s): {string.Join(" ", mostFittest.DecodedSubValues.Select(v => v).ToArray())} | target path: {targetPath} | execution path: {mostFittest.ExecutionPath} | fitness: {mostFittest.Fitness} | result: {mostFittest.Result}";
                    writer.WriteLine(line);
                    // 以上为写文件操作

                    //以下为终止条件
                    if (mostFittest.ExecutionPath.ToString().Contains(targetPath))
                    {
                        //将找到的数据添加到测试数据集中
                        var assertion = new AssertionInfo();
                        assertion.InputValues.AddRange(mostFittest.DecodedSubValues.Select(v => v).ToList());
                        assertions.Add(assertion);

                        writer.WriteLine("-----------------FOUND-----------------");

                        break;
                    }
                    //进化
                    population.Evolve(gaParameters.SelectionType);
                }
            }

            writer.Close();

            return assertions;
        }
    }
}