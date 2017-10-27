using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Branch1 : AbstractFunction
    {
        public override object GetResult()
        {
            var x = Paras[0];
            var y = Paras[1];
            var result = 0;

            if (x >= 80)
            {
                result = 1;
                if (y < 50)
                    result = 2;
            }

            return result;
        }

        public override double GetFitness()
        {
            if (FitnessCaculationType == FitnessType.Basic)
                return GetFitnessByCoverageRate();
            if (FitnessCaculationType == FitnessType.Distance)
                return GetFitnessByDistance();

            return double.MinValue;
        }

        private double GetFitnessByCoverageRate()
        {
            var x = Paras[0];
            var y = Paras[1];
            var result = 0;
            var path = "#";

            if (x >= 80)
            {
                result = 1;
                path += "a";
                if (y < 50)
                {
                    result = 2;
                    path += "b";
                }
            }

            return path.Length / (double) "#ab".Length;
        }


        public double GetFitnessByDistance()
        {
            var x = Paras[0];
            var y = Paras[1];
            var result = 0;

            if (x >= 80)
            {
                result = 1;
                if (y < 50)
                    result = 2;
            }

            var conditions = new List<ConditionInfo>
            {
                new ConditionInfo {A = x, B = 80, Oper = ">="},
                new ConditionInfo {A = y, B = 50, Oper = "<"}
            };

            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
        }
    }
}