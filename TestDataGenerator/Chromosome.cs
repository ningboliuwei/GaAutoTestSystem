using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDataGenerator
{
    public class Chromosome
    {
        public Population Population { get; set; }

        public long Value { get; set; }

        public List<int> SubValues
        {
            get
            {
                //根据染色体得到级联的多参数的各个参数的值（解码前）
                var subValues = new List<int>();
                var valueBinaryString = Convert.ToString(Value, 2).PadLeft(Population.ChromosomeLength, '0');

                //获取每个片段的值
                for (var i = 0; i < Population.SubValueQuantity; i++)
                {
                    var currentSubValueBinaryString = valueBinaryString.Substring(
                        Population.ChromosomeLengthForOneSubValue * i,
                        Population.ChromosomeLengthForOneSubValue);

                    subValues.Add(Convert.ToInt32(currentSubValueBinaryString, 2));
                }

                return subValues;
            }
        }

        //解码后的子值
        public List<double> DecodedSubValues
        {
            get
            {
                var paras = Population.RelatedFunction.Paras;
                var decodedSubValues = new List<double>();

                for (var i = 0; i < SubValues.Count; i++)
                {
                    var subValue = SubValues[i];
                    var decodedSubValue = GetDecodedValue(subValue, paras[i].LowerBound,
                        paras[i].UpperBound);

                    if (paras[i].DataType == ParaDataType.Double)
                    {
                        decodedSubValues.Add(decodedSubValue);
                    }
                    else if (paras[i].DataType == ParaDataType.Integer)
                    {
                        decodedSubValues.Add((int) decodedSubValue);
                    }
                }

                return decodedSubValues;
            }
        }

        public double Fitness
        {
            get
            {
                var paras = Population.RelatedFunction.Paras;

                for (var i = 0; i < paras.Count; i++)
                    paras[i].Value = SubValues.Select(v => GetDecodedValue(v, paras[i].LowerBound, paras[i].UpperBound))
                        .ToList()[i];

                return Population.RelatedFunction.GetFitness();
            }
        }

        public object Result
        {
            get
            {
                var paras = Population.RelatedFunction.Paras;

                for (var i = 0; i < paras.Count; i++)
                    paras[i].Value = SubValues.Select(v => GetDecodedValue(v, paras[i].LowerBound, paras[i].UpperBound))
                        .ToList()[i];

                return Population.RelatedFunction.GetResult();
            }
        }

        public object ExecutionPath
        {
            get
            {
                var paras = Population.RelatedFunction.Paras;

                for (var i = 0; i < paras.Count; i++)
                    paras[i].Value = SubValues.Select(v => GetDecodedValue(v, paras[i].LowerBound, paras[i].UpperBound))
                        .ToList()[i];

                return Population.RelatedFunction.ExecutionPath;
            }
        }

        //得到将染色体值转换为在解空间对应的值
        public double GetDecodedValue(double value, double lowerBound, double upperBound)
        {
            return lowerBound + value * (upperBound - lowerBound) /
                   (Math.Pow(2, Convert.ToInt32(Population.ChromosomeLength / Population.SubValueQuantity)) - 1);
        }
    }
}