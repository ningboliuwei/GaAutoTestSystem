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

        public double Fitness
        {
            get
            {
                Population.RelatedFunction.Paras = SubValues.Select(v => GetDecodedValue(v)).ToList();
                
                return Population.RelatedFunction.GetFitness();
            }
        }

        public object Result
        {
            get
            {
                Population.RelatedFunction.Paras = SubValues.Select(v => GetDecodedValue(v)).ToList();

                return Population.RelatedFunction.GetResult();
            }
        }

        //得到将染色体值转换为在解空间对应的值
        public double GetDecodedValue(double value)
        {
            return Population.SolutionLowerBound + value * (Population.SolutionUpperBound -
                                                            Population.SolutionLowerBound) /
                   (Math.Pow(2, Convert.ToInt32(Population.ChromosomeLength / Population.SubValueQuantity)) - 1);
        }
    }
}