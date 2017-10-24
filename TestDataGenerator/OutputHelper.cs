using System;
using System.Linq;
using System.Text;

namespace TestDataGenerator
{
    public class OutputHelper
    {
        public static string GetChromosomeBinaryValue(Chromosome chromosome)
        {
            return Convert.ToString(chromosome.Value, 2)
                .PadLeft(chromosome.Population.ChromosomeLength, '0');
        }

        public static string GetChromosomeSubValues(Chromosome chromosome)
        {
            var s = "";

            foreach (var value in chromosome.SubValues)
                s += value + " ";

            return s;
        }

        public static string GetChromosomeInfo(Chromosome chromosome,
            Population.FitnessFunctionDelegate fitnessFunction)
        {
            var builder = new StringBuilder();

            //所有映射到解空间的值（若有级联）
            var decodedSubValues = chromosome.SubValues.Select(v =>
                chromosome.GetDecodedValue(v)).ToArray();
            var decodedSubValuesString = string.Join(" ", decodedSubValues);

            //染色体二进制表示 | 所有子值（即多输入参数拼接） | 适应度
            builder.Append(
                $"{GetChromosomeBinaryValue(chromosome)} | value(s): {decodedSubValuesString} | fitness: {chromosome.Fitness}");
            // builder.Append ($"fitness: {TestFunction.StubbedTriangleTypeTestPathCoverage(a, b, c)} ");
            builder.Append($" | result: {fitnessFunction(decodedSubValues)} ");
            // builder.Append ($"path: {TestFunction.TriangleTypeTestPathCoverage(a, b, c)}");
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
    }
}