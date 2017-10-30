using System;
using System.Collections.Generic;
using System.Text;

namespace TestDataGenerator
{
    public class OutputHelper
    {
        public static string GetChromosomeInfo(Chromosome chromosome)
        {
            var builder = new StringBuilder();
            var decodedSubValues = new List<double>();
            var paras = chromosome.Population.RelatedFunction.Paras;

            for (var i = 0; i < paras.Count; i++)
            {
                decodedSubValues.Add(chromosome.GetDecodedValue(chromosome.SubValues[i], paras[i].LowerBound,
                    paras[i].UpperBound));
            }
            //所有映射到解空间的值（若有级联）
            var decodedSubValuesString = string.Join(" ", decodedSubValues.ToArray());
            var chromosomeBinaryValue = Convert.ToString(chromosome.Value, 2)
                .PadLeft(chromosome.Population.ChromosomeLength, '0');

            //染色体二进制表示 | 所有子值（即多输入参数拼接） | 适应度
            builder.Append(
                $"{chromosomeBinaryValue} | value(s): {decodedSubValuesString} | fitness: {chromosome.Fitness}");
            builder.Append($" | execution path: {chromosome.ExecutionPath} ");
            builder.Append($" | result: {chromosome.Result} ");
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
    }
}