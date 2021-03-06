﻿using System;
using System.Collections.Generic;

namespace TestDataGenerator
{
    public class Chromosome
    {
        public Population Population { get; set; }

        public long Value { get; set; }

        private int[] SubValues
        {
            get
            {
                //根据染色体得到级联的多参数的各个参数的值（解码前）
                var subValues = new int[Population.SubValueQuantity];
                var valueBinaryString = Convert.ToString(Value, 2).PadLeft(Population.ChromosomeLength, '0');

                //获取每个片段的值
                for (var i = 0; i < subValues.Length; i++)
                {
                    var currentSubValueBinaryString = valueBinaryString.Substring(
                        Population.ChromosomeLengthForOneSubValue * i,
                        Population.ChromosomeLengthForOneSubValue);

                    subValues[i] = Convert.ToInt32(currentSubValueBinaryString, 2);
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

                var subValueCount = SubValues.Length;
                var subValues = SubValues;

                for (var i = 0; i < subValueCount; i++)
                {
                    var subValue = subValues[i];
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

        public double Fitness => Population.RelatedFunction.GetFitness(this);

        public string ExecutionPath => Population.RelatedFunction.GetExecutionPath(this);

        public object Result => Population.RelatedFunction.GetResult(this);

        //得到将染色体值转换为在解空间对应的值
        private double GetDecodedValue(double value, double lowerBound, double upperBound)
        {
            return lowerBound + value * (upperBound - lowerBound) /
                   (Math.Pow(2, Convert.ToInt32(Population.ChromosomeLength / Population.SubValueQuantity)) - 1);
        }
    }
}