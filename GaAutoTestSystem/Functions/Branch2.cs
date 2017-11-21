using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Branch2 : AbstractFunction
    {
        public override object OriginalFunction(params double[] paras)
        {
            double k = 0;
            double j = 0;
            var x = (int) paras[0];
            var y = (int) paras[1];
            var z = (int) paras[2];

            if (x > 1 && z < 10)
            {
                k = x + z;
                j = k * k;
            }

            if (y == 4 || z > 1)
                j = z * y + 10;

            j = j % 3;

            return j;
        }

        public override string StubbedFunction(params double[] paras)
        {
            var x = (int) paras[0];
            var y = (int) paras[1];
            var z = (int) paras[2];
            var path = "#";

            if (x > 1)
            {
                path += "a";
                if (z < 10)
                    path += "b";
            }

            if (y == 4)
            {
                path += "c";
            }
            else
            {
                path += "d";
                if (z > 1)
                    path += "e";
            }

            path += "f";
            return path;
        }

        protected override double GetFitnessByCoverageRate(Chromosome chromosome)
        {
            return GetExecutionPath(chromosome).Length / (double) "#abcde".Length;
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