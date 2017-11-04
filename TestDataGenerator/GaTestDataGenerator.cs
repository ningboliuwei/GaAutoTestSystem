using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{
    public class GaTestDataGenerator
    {
        public static List<AssertionInfo> GetAssertions(GaParameterInfo gaParameters, AbstractFunction function, List<string> targetPaths)
        {
            //新建一个种群
            var population = new Population(gaParameters, function);
            var assertions = new List<AssertionInfo>();

            //随机生成染色体
            population.RandomGenerateChromosome();

            foreach (var targetPath in targetPaths)
            {
                for (var i = 0; i < gaParameters.GenerationQuantity; i++)
                {
                    var maxFitness = population.Chromosomes.Max(n => n.Fitness);
                    var mostFittest = population.Chromosomes.First(c => Equals(c.Fitness, maxFitness));
                
                    population.Evolve(gaParameters.SelectionType);

                    //以下为终止条件
                    if (mostFittest.ExecutionPath.ToString().Contains(targetPath))
                    {
                        //将找到的数据添加到测试数据集中
                        var assertion = new AssertionInfo();
                        assertion.InputValues.AddRange(mostFittest.DecodedSubValues.Select(v => v).ToList());
                        assertions.Add(assertion);
                      
                        break;
                    }
                }
            }

            return assertions;
        }
    }
}
