using System;
using System.Text;

namespace TestDataGenerator
{
    public class OutputHelper
    {
        public static string GetChromosomeInfo(Chromosome chromosome)
        {
            var builder = new StringBuilder();
            var paras = chromosome.Population.RelatedFunction.Paras;
            var decodedSubValuesString = "";
            //所有映射到解空间的值（若有级联） 
            for (var i = 0; i < paras.Count; i++)
            {
                var decodedSubValue = chromosome.DecodedSubValues[i];
                if (paras[i].DataType == ParaDataType.Double)
                {
                    decodedSubValuesString += $" {decodedSubValue}";
                }
                else if (paras[i].DataType == ParaDataType.Integer)
                {
                    decodedSubValuesString += $" {(long) decodedSubValue}";
                }
            }

            var chromosomeBinaryValue = Convert.ToString(chromosome.Value, 2)
                .PadLeft(chromosome.Population.ChromosomeLength, '0');

            //染色体二进制表示 | 所有子值（即多输入参数拼接） | 适应度
            builder.Append(
                $"{chromosomeBinaryValue} | value(s): {decodedSubValuesString} | fitness: {chromosome.Fitness}");

            builder.Append(Environment.NewLine);

            return builder.ToString();
        }

        public static string GetFunctionResultInfo(AbstractFunction function, AssertionInfo assertion)
        {
            var paras = function.Paras;

            for (var i = 0; i < paras.Count; i++)
                paras[i].Value = assertion.InputValues[i];

            return $" execution path: {function.ExecutionPath} | result: {function.Result}";
        }
    }
}