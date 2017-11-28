using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Branch1 : AbstractFunction
    {
        public override object OriginalFunction(params double[] paras)
        {
            var x = (int) paras[0];
            var y = (int) paras[1];
            var result = 0;

            if (x >= 80)
            {
                result = 1;
                {
                    if (y < 50)
                    {
                        result = 2;
                    }
                }
            }

            return result;
        }

        public override string StubbedFunction(params double[] paras)
        {
            var x = (int) paras[0];
            var y = (int) paras[1];
            // var result = 0;
            var path = "a";
            
            if (x >= 80)
            {
                path += "b";
                // result = 1;
                {
                    path += "c";
                    if (y < 50)
                    {
                        path += "d";
                        // result = 2;
                    }
                }
            }
            path += "e";

            return path;
        }

        protected override double GetFitnessByCoverageRate(Chromosome chromosome)
        {
            return GetExecutionPath(chromosome).Length / (double) "#abc".Length;
        }

        protected override double GetFitnessByDistance(Chromosome chromosome)
        {
            var x = (int) chromosome.DecodedSubValues[0];
            var y = (int) chromosome.DecodedSubValues[1];

            var conditions = new List<ConditionInfo>
            {
                new ConditionInfo {A = x, B = 80, Oper = ">="},
                new ConditionInfo {A = y, B = 50, Oper = "<"}
            };

            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
        }
    }
}