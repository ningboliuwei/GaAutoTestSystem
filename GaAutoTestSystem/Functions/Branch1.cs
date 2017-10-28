using System;
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

        protected override string GetExecutionPath()
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

            return path;
        }

        protected override double GetFitnessByNodeMatch()
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByCoverageRate()
        {
            return GetExecutionPath().Length / (double) "#ab".Length;
        }


        protected override double GetFitnessByDistance()
        {
            var x = Paras[0];
            var y = Paras[1];

            var conditions = new List<ConditionInfo>
            {
                new ConditionInfo {A = x, B = 80, Oper = ">="},
                new ConditionInfo {A = y, B = 50, Oper = "<"}
            };

            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
        }

        protected override double GetFitnessByPathMatch()
        {
            throw new NotImplementedException();
        }
    }
}