﻿using System;
using System.Linq;
using System.Text;

namespace TestDataGenerator
{
    public class OutputHelper
    {
        public static string GetChromosomeInfo(Chromosome chromosome)
        {
            var builder = new StringBuilder();

            //所有映射到解空间的值（若有级联）
            var decodedSubValuesString = string.Join(" ", chromosome.SubValues.Select(v =>
                chromosome.GetDecodedValue(v)).ToArray());
            var chromosomeBinaryValue = Convert.ToString(chromosome.Value, 2)
                .PadLeft(chromosome.Population.ChromosomeLength, '0');

            //染色体二进制表示 | 所有子值（即多输入参数拼接） | 适应度
            builder.Append(
                $"{chromosomeBinaryValue} | value(s): {decodedSubValuesString} | fitness: {chromosome.Fitness}");
            builder.Append($" | result: {chromosome.Result} ");
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
    }
}